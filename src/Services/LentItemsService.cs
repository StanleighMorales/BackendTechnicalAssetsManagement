using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs;
using BackendTechnicalAssetsManagement.src.DTOs.Archive.Items;
using BackendTechnicalAssetsManagement.src.DTOs.Archive.LentItems;
using BackendTechnicalAssetsManagement.src.DTOs.LentItems;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Utils;
using TechnicalAssetManagementApi.Dtos.Item;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.Services
{ 
    public class LentItemsService : ILentItemsService
    {
        private readonly ILentItemsRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IArchiveLentItemsService _archiveLentItemsService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IActivityLogService _activityLogService;
        private readonly ISupabaseStorageService _storageService;

        public LentItemsService(ILentItemsRepository repository, IMapper mapper, IUserRepository userRepository, IItemRepository itemRepository, IArchiveLentItemsService archiveLentItemsService, IUserService userService, INotificationService notificationService, IActivityLogService activityLogService, ISupabaseStorageService storageService)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _itemRepository = itemRepository;
            _archiveLentItemsService = archiveLentItemsService;
            _userService = userService;
            _notificationService = notificationService;
            _activityLogService = activityLogService;
            _storageService = storageService;
        }

        // Create
        public async Task<LentItemsDto> AddForGuestAsync(CreateLentItemsForGuestDto dto, Guid issuedById)
        {
            // Resolve the issuing staff/admin's last name for accountability
            var issuer = await _userRepository.GetByIdAsync(issuedById);
            var issuedByLastName = issuer?.LastName ?? string.Empty;
            // 1. Resolve item from RFID tag scan instead of a pre-selected ItemId
            var item = await _itemRepository.GetByRfidUidAsync(dto.TagUid);
            if (item == null)
                throw new KeyNotFoundException($"No item found with RFID tag '{dto.TagUid}'.");

            // 2. Item availability checks
            if (item.Condition == ItemCondition.Defective || item.Condition == ItemCondition.NeedRepair)
                throw new InvalidOperationException($"Item '{item.ItemName}' is in {item.Condition} condition and cannot be lent.");

            if (item.Status == ItemStatus.Borrowed || item.Status == ItemStatus.Reserved || item.Status == ItemStatus.Unavailable)
                throw new InvalidOperationException($"Item '{item.ItemName}' is already {item.Status.ToString().ToLower()} and cannot be lent.");

            var allLentItems = await _repository.GetActiveByItemIdLightAsync(item.Id);

            var activeLentItem = allLentItems.FirstOrDefault(li =>
                li.Status == "Pending" || li.Status == "Reserved" || li.Status == "Approved" || li.Status == "Borrowed");

            if (activeLentItem != null)
                throw new InvalidOperationException($"Item '{item.ItemName}' already has an active lent record (Status: {activeLentItem.Status}).");

            if (dto.ReservedFor.HasValue)
            {
                var isAvailable = await IsItemAvailableForReservation(item.Id, dto.ReservedFor, allLentItems);
                if (!isAvailable)
                    throw new InvalidOperationException($"Item '{item.ItemName}' is already reserved or borrowed and cannot be reserved at this time.");
            }

            // 3. Build the entity — AutoMapper handles simple field copies, we set the rest manually
            var lentItem = _mapper.Map<LentItems>(dto);

            lentItem.ItemId = item.Id;
            lentItem.ItemName = item.ItemName;
            lentItem.TagUid = dto.TagUid;
            lentItem.BorrowerFullName = $"{dto.BorrowerFirstName} {dto.BorrowerLastName}";
            lentItem.BorrowerRole = "Guest";
            lentItem.UserId = null;
            lentItem.TeacherId = null;
            if (dto.GuestImage != null)
                lentItem.GuestImageUrl = await _storageService.UploadImageAsync(dto.GuestImage, "guests");
            lentItem.Organization = dto.Organization;
            lentItem.ContactNumber = dto.ContactNumber;
            lentItem.Purpose = dto.Purpose;
            lentItem.TeacherFullName = dto.SupervisorName;
            lentItem.IssuedById = issuedById;
            lentItem.IssuedByLastName = issuedByLastName;

            // 4. Determine status from intent: if ReservedFor is set it's a reservation (Pending),
            //    otherwise it's an immediate borrow. Never trust the client-supplied Status string
            //    for security — the backend owns this decision.
            bool isReservation = dto.ReservedFor.HasValue ||
                                  dto.Status.Equals("Reserved", StringComparison.OrdinalIgnoreCase) ||
                                  dto.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase);

            if (!isReservation)
            {
                item.Status = ItemStatus.Borrowed;
                item.UpdatedAt = DateTime.UtcNow;
                lentItem.LentAt = DateTime.UtcNow;
                lentItem.Status = LentItemsStatus.Borrowed.ToString();
                await _itemRepository.UpdateAsync(item);
            }
            else
            {
                // Guest reservations always start as Pending — staff must approve before pickup.
                // Mark item as Unavailable immediately so no one else can borrow or reserve it
                // while the request is pending. It becomes Reserved on Pending → Approved,
                // and Available again if Denied or Canceled.
                item.Status = ItemStatus.Unavailable;
                item.UpdatedAt = DateTime.UtcNow;
                lentItem.Status = LentItemsStatus.Pending.ToString();
                await _itemRepository.UpdateAsync(item);
            }

            // 5. Persist
            await _repository.AddAsync(lentItem);
            await _repository.SaveChangesAsync();

            var createdItem = await _repository.GetByIdAsync(lentItem.Id);

            // 6. Activity log
            var guestLogCategory = lentItem.Status switch
            {
                "Borrowed" => ActivityLogCategory.BorrowedItem,
                "Approved" => ActivityLogCategory.Approved,
                "Denied"   => ActivityLogCategory.Denied,
                "Canceled" => ActivityLogCategory.Canceled,
                "Expired"  => ActivityLogCategory.Expired,
                _          => ActivityLogCategory.General
            };
            await WriteLogAsync(guestLogCategory, $"Guest borrow request created with status '{lentItem.Status}'", lentItem, null, lentItem.Status);

            return _mapper.Map<LentItemsDto>(createdItem);
        }


        // ── Instant Borrow ────────────────────────────────────────────────────────
        public async Task<LentItemsDto> AddBorrowAsync(CreateBorrowDto dto)
        {
            // 1. Validate item
            var item = await _itemRepository.GetByIdAsync(dto.ItemId);
            if (item == null)
                throw new KeyNotFoundException($"Item with ID {dto.ItemId} not found.");

            if (item.Condition == ItemCondition.Defective || item.Condition == ItemCondition.NeedRepair)
                throw new InvalidOperationException($"Item '{item.ItemName}' is in {item.Condition} condition and cannot be lent.");

            // Load only active records for this item — avoids full table scan
            var activeItemRecords = await _repository.GetActiveByItemIdLightAsync(dto.ItemId);

            // Check if the user has an existing reservation for this item
            if (dto.UserId.HasValue)
            {
                var userReservation = activeItemRecords.FirstOrDefault(li =>
                    li.UserId == dto.UserId.Value &&
                    (li.Status == "Pending" || li.Status == "Approved"));

                // If user has a reservation, convert it to Borrowed instead of creating new record
                if (userReservation != null)
                {
                    // Re-fetch tracked version for update
                    var trackedReservation = await _repository.GetByIdAsync(userReservation.Id);
                    if (trackedReservation != null)
                    {
                        trackedReservation.Status = LentItemsStatus.Borrowed.ToString();
                        trackedReservation.LentAt = DateTime.UtcNow;
                        trackedReservation.Room = dto.Room ?? trackedReservation.Room;
                        trackedReservation.SubjectTimeSchedule = dto.SubjectTimeSchedule ?? trackedReservation.SubjectTimeSchedule;
                        trackedReservation.UpdatedAt = DateTime.UtcNow;

                        item.Status = ItemStatus.Borrowed;
                        item.UpdatedAt = DateTime.UtcNow;
                        await _itemRepository.UpdateAsync(item);

                        await _repository.UpdateAsync(trackedReservation);
                        await _repository.SaveChangesAsync();

                        await _notificationService.SendItemBorrowedNotificationAsync(
                            trackedReservation.Id, trackedReservation.UserId, trackedReservation.ItemName, trackedReservation.BorrowerFullName ?? "Unknown");

                        await WriteLogAsync(ActivityLogCategory.BorrowedItem,
                            $"Reservation converted to borrowed via RFID scan", trackedReservation, "Pending/Approved", trackedReservation.Status);

                        return _mapper.Map<LentItemsDto>(trackedReservation);
                    }
                }
            }

            // No reservation found, proceed with normal borrow flow
            if (item.Status == ItemStatus.Borrowed || item.Status == ItemStatus.Reserved || item.Status == ItemStatus.Unavailable)
                throw new InvalidOperationException($"Item '{item.ItemName}' is already {item.Status.ToString().ToLower()} and cannot be lent.");

            var activeLentItem = activeItemRecords.FirstOrDefault(li =>
                li.Status == "Pending" || li.Status == "Approved" || li.Status == "Borrowed");

            if (activeLentItem != null)
                throw new InvalidOperationException($"Item '{item.ItemName}' already has an active lent record (Status: {activeLentItem.Status}).");

            // 2. Validate borrower
            if (dto.UserId.HasValue)
            {
                var (isComplete, profileError) = await _userService.ValidateStudentProfileComplete(dto.UserId.Value);
                if (!isComplete)
                    throw new InvalidOperationException($"Cannot borrow item. {profileError}");

                var user = await _userRepository.GetByIdAsync(dto.UserId.Value);
                if (user == null)
                    throw new KeyNotFoundException($"User with ID {dto.UserId.Value} not found.");

                if (user.UserRole == UserRole.Teacher || user.UserRole == UserRole.Student)
                {
                    var activeUserCount = await _repository.GetActiveByUserIdLightAsync(dto.UserId.Value);
                    if (activeUserCount.Count() >= 3)
                        throw new InvalidOperationException($"Borrowing limit reached. {user.UserRole}s can only have a maximum of 3 active requests at a time.");
                }
            }

            if (dto.TeacherId.HasValue)
            {
                var teacher = await _userRepository.GetByIdAsync(dto.TeacherId.Value) as Teacher;
                if (teacher == null)
                    throw new KeyNotFoundException($"Teacher with ID {dto.TeacherId.Value} not found.");
            }

            // 3. Build entity — backend owns status (always Borrowed for RFID instant borrow)
            var lentItem = _mapper.Map<LentItems>(dto);
            lentItem.ItemName = item.ItemName;
            lentItem.Status = LentItemsStatus.Borrowed.ToString();
            lentItem.LentAt = DateTime.UtcNow;

            await PopulateBorrowerInfoAsync(lentItem, dto.UserId, dto.TeacherId, null);

            // 4. Mark item as Borrowed immediately — user is physically taking it now
            item.Status = ItemStatus.Borrowed;
            item.UpdatedAt = DateTime.UtcNow;
            await _itemRepository.UpdateAsync(item);

            await _repository.AddAsync(lentItem);
            await _repository.SaveChangesAsync();

            await _notificationService.SendItemBorrowedNotificationAsync(
                lentItem.Id, lentItem.UserId, lentItem.ItemName, lentItem.BorrowerFullName ?? "Unknown");

            await WriteLogAsync(ActivityLogCategory.BorrowedItem,
                $"Item borrowed via RFID scan with status '{lentItem.Status}'", lentItem, null, lentItem.Status);

            return _mapper.Map<LentItemsDto>(lentItem);
        }

        // ── Reservation ───────────────────────────────────────────────────────────
        public async Task<LentItemsDto> AddReservationAsync(CreateReservationDto dto)
        {
            // 1. ReservedFor must be in the future and within 7 days from now
            if (dto.ReservedFor <= DateTime.UtcNow)
                throw new InvalidOperationException("ReservedFor must be a future date and time.");

            if (dto.ReservedFor > DateTime.UtcNow.AddDays(7))
                throw new InvalidOperationException("Reservations can only be made up to 7 days in advance.");

            // 2. Validate item
            var item = await _itemRepository.GetByIdAsync(dto.ItemId);
            if (item == null)
                throw new KeyNotFoundException($"Item with ID {dto.ItemId} not found.");

            if (item.Condition == ItemCondition.Defective || item.Condition == ItemCondition.NeedRepair)
                throw new InvalidOperationException($"Item '{item.ItemName}' is in {item.Condition} condition and cannot be reserved.");

            if (item.Status == ItemStatus.Borrowed || item.Status == ItemStatus.Unavailable)
                throw new InvalidOperationException($"Item '{item.ItemName}' is currently {item.Status.ToString().ToLower()} and cannot be reserved.");

            var activeItemRecords = await _repository.GetActiveByItemIdLightAsync(dto.ItemId);

            var activeLentItem = activeItemRecords.FirstOrDefault(li =>
                li.Status == "Pending" || li.Status == "Approved" || li.Status == "Borrowed");

            if (activeLentItem != null)
                throw new InvalidOperationException($"Item '{item.ItemName}' already has an active lent record (Status: {activeLentItem.Status}).");

            var isAvailable = await IsItemAvailableForReservation(dto.ItemId, dto.ReservedFor, activeItemRecords);
            if (!isAvailable)
                throw new InvalidOperationException($"Item '{item.ItemName}' is already reserved or borrowed and cannot be reserved at this time.");

            // 3. Validate borrower
            if (dto.UserId.HasValue)
            {
                var (isComplete, profileError) = await _userService.ValidateStudentProfileComplete(dto.UserId.Value);
                if (!isComplete)
                    throw new InvalidOperationException($"Cannot reserve item. {profileError}");

                var user = await _userRepository.GetByIdAsync(dto.UserId.Value);
                if (user == null)
                    throw new KeyNotFoundException($"User with ID {dto.UserId.Value} not found.");

                if (user.UserRole == UserRole.Teacher || user.UserRole == UserRole.Student)
                {
                    var activeUserRecords = await _repository.GetActiveByUserIdLightAsync(dto.UserId.Value);
                    if (activeUserRecords.Count() >= 3)
                        throw new InvalidOperationException($"Borrowing limit reached. {user.UserRole}s can only have a maximum of 3 active requests at a time.");
                }
            }

            if (dto.TeacherId.HasValue)
            {
                var teacher = await _userRepository.GetByIdAsync(dto.TeacherId.Value) as Teacher;
                if (teacher == null)
                    throw new KeyNotFoundException($"Teacher with ID {dto.TeacherId.Value} not found.");
            }

            // 4. Build entity — backend owns status (always Pending for new reservations)
            var lentItem = _mapper.Map<LentItems>(dto);
            lentItem.ItemName = item.ItemName;
            lentItem.Status = LentItemsStatus.Pending.ToString();

            await PopulateBorrowerInfoAsync(lentItem, dto.UserId, dto.TeacherId, null);

            // 5. Mark item as Unavailable immediately so no one else can borrow or reserve it
            //    while the request is pending admin approval.
            //    The item will be set to Reserved when UpdateAsync transitions Pending → Approved,
            //    and back to Available if the request is Denied or Canceled.
            item.Status = ItemStatus.Unavailable;
            item.UpdatedAt = DateTime.UtcNow;
            await _itemRepository.UpdateAsync(item);

            await _repository.AddAsync(lentItem);
            await _repository.SaveChangesAsync();

            // 6. Notify admin/staff
            await _notificationService.SendNewPendingRequestNotificationAsync(
                lentItem.Id, lentItem.ItemName, lentItem.BorrowerFullName ?? "Unknown", lentItem.ReservedFor);

            await WriteLogAsync(ActivityLogCategory.General,
                $"Reservation created for {dto.ReservedFor:yyyy-MM-dd HH:mm} with status '{lentItem.Status}'", lentItem, null, lentItem.Status);

            return _mapper.Map<LentItemsDto>(lentItem);
        }

        // ── Shared helper ─────────────────────────────────────────────────────────
        private async Task PopulateBorrowerInfoAsync(LentItems lentItem, Guid? userId, Guid? teacherId, IEnumerable<LentItems>? allLentItems)
        {
            if (userId.HasValue)
            {
                var user = await _userRepository.GetByIdAsync(userId.Value);
                if (user != null)
                {
                    lentItem.BorrowerFullName = $"{user.FirstName} {user.LastName}";
                    lentItem.BorrowerRole = user.UserRole.ToString();

                    if (user is Student student)
                    {
                        lentItem.StudentIdNumber = student.StudentIdNumber;
                        lentItem.FrontStudentIdPictureUrl = student.FrontStudentIdPictureUrl;
                    }
                }
            }

            if (teacherId.HasValue)
            {
                var teacher = await _userRepository.GetByIdAsync(teacherId.Value) as Teacher;
                if (teacher != null)
                {
                    lentItem.Teacher = teacher;
                    lentItem.TeacherFullName = $"{teacher.FirstName} {teacher.LastName}";
                }
            }
        }


        // Read
        public async Task<IEnumerable<LentItemsDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<LentItemsDto>>(items);
        }

        public async Task<IEnumerable<LentItemsDto>> GetAllBorrowedItemsAsync()
        {
            var items = await _repository.GetAllBorrowedItemsAsync();
            return _mapper.Map<IEnumerable<LentItemsDto>>(items);
        }

        public async Task<LentItemsDto?> GetByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            return _mapper.Map<LentItemsDto?>(item);
        }

        public async Task<IEnumerable<LentItemsDto>> GetByDateTimeAsync(DateTime dateTime)
        {
            var items = await _repository.GetByDateTime(dateTime);
            return _mapper.Map<IEnumerable<LentItemsDto>>(items);
        }

        // Update
        public async Task<bool> UpdateAsync(Guid id, UpdateLentItemDto dto)
        {
            // 1. Fetch the existing entity from the database
            var entity = await _repository.GetByIdAsync(id);

            // 2. Check if it exists
            if (entity == null)
            {
                // Return false or throw a KeyNotFoundException, your choice
                return false;
            }

            // 3. Handle status changes and corresponding item status updates
            if (!string.IsNullOrEmpty(dto.Status))
            {
                var oldStatus = entity.Status ?? string.Empty;
                var newStatus = dto.Status;

                // If status is changing, update the corresponding item status
                if (!string.Equals(oldStatus, newStatus, StringComparison.OrdinalIgnoreCase))
                {
                    var item = await _itemRepository.GetByIdAsync(entity.ItemId);
                    if (item != null)
                    {
                        // Check if item condition is broken when trying to borrow, approve, or set to pending
                        if ((newStatus.Equals("Borrowed", StringComparison.OrdinalIgnoreCase) || 
                             newStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase) ||
                             newStatus.Equals("Approved", StringComparison.OrdinalIgnoreCase)) &&
                            (item.Condition == ItemCondition.Defective || item.Condition == ItemCondition.NeedRepair))
                        {
                            throw new InvalidOperationException($"Item '{item.ItemName}' is in {item.Condition} condition and cannot be lent.");
                        }

                        // Check if trying to set an active status on an item that's already borrowed or reserved
                        if ((newStatus.Equals("Borrowed", StringComparison.OrdinalIgnoreCase) || 
                             newStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase) ||
                             newStatus.Equals("Approved", StringComparison.OrdinalIgnoreCase)) && 
                            (item.Status == ItemStatus.Borrowed || item.Status == ItemStatus.Reserved || item.Status == ItemStatus.Unavailable) && 
                            !oldStatus.Equals("Borrowed", StringComparison.OrdinalIgnoreCase) &&
                            !oldStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase) &&
                            !oldStatus.Equals("Approved", StringComparison.OrdinalIgnoreCase))
                        {
                            throw new InvalidOperationException($"Item '{item.ItemName}' is already {item.Status.ToString().ToLower()} and cannot be lent.");
                        }

                        // Update item status based on new lent item status
                        if (newStatus.Equals("Borrowed", StringComparison.OrdinalIgnoreCase))
                        {
                            item.Status = ItemStatus.Borrowed;
                            item.UpdatedAt = DateTime.UtcNow;
                            entity.LentAt = DateTime.UtcNow;
                        }
                        else if (newStatus.Equals("Approved", StringComparison.OrdinalIgnoreCase))
                        {
                            item.Status = ItemStatus.Reserved;
                            item.UpdatedAt = DateTime.UtcNow;
                            entity.LentAt = null;
                            
                            // Send approval notification when status changes from Pending to Approved
                            if (oldStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                            {
                                await _notificationService.SendApprovalNotificationAsync(
                                    entity.Id, 
                                    entity.UserId, 
                                    entity.ItemName ?? item.ItemName, 
                                    entity.BorrowerFullName ?? "Unknown"
                                );
                            }
                        }
                        else if (newStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                        {
                            item.Status = ItemStatus.Unavailable;
                            item.UpdatedAt = DateTime.UtcNow;
                            entity.LentAt = null;
                        }
                        else if (newStatus.Equals("Returned", StringComparison.OrdinalIgnoreCase))
                        {
                            item.Status = ItemStatus.Available;
                            item.UpdatedAt = DateTime.UtcNow;
                            entity.ReturnedAt = DateTime.UtcNow;
                        }
                        else if (newStatus.Equals("Canceled", StringComparison.OrdinalIgnoreCase))
                        {
                            item.Status = ItemStatus.Available;
                            item.UpdatedAt = DateTime.UtcNow;
                            entity.LentAt = null;
                        }
                        else if (newStatus.Equals("Denied", StringComparison.OrdinalIgnoreCase))
                        {
                            item.Status = ItemStatus.Available;
                            item.UpdatedAt = DateTime.UtcNow;
                            entity.LentAt = null;
                        }

                        await _itemRepository.UpdateAsync(item);
                    }
                    
                    // Send general status change notification for all status changes
                    await _notificationService.SendStatusChangeNotificationAsync(
                        entity.Id,
                        entity.UserId,
                        entity.ItemName ?? "Unknown Item",
                        oldStatus,
                        newStatus
                    );

                    // Log the status transition
                    var updateLogCategory = newStatus switch
                    {
                        "Borrowed" => ActivityLogCategory.BorrowedItem,
                        "Returned" => ActivityLogCategory.Returned,
                        "Approved" => ActivityLogCategory.Approved,
                        "Denied"   => ActivityLogCategory.Denied,
                        "Canceled" => ActivityLogCategory.Canceled,
                        "Expired"  => ActivityLogCategory.Expired,
                        _          => ActivityLogCategory.StatusChange
                    };
                    await WriteLogAsync(updateLogCategory, $"Status changed from '{oldStatus}' to '{newStatus}'", entity, oldStatus, newStatus);
                }
            }

            // 4. Apply the DTO properties onto the fetched entity
            // This special overload of AutoMapper is designed for this exact purpose.
            // It will only update the properties that are not null in the DTO.
            _mapper.Map(dto, entity);

            // Note: If you need complex logic (like updating BorrowerFullName when UserId changes),
            // you would add that logic here before saving.

            // 5. Update the entity in the context and save
            await _repository.UpdateAsync(entity);
            return await _repository.SaveChangesAsync();
        }
        // Delete (soft & hard)
        public async Task<bool> SoftDeleteAsync(Guid id)
        {
            await _repository.SoftDeleteAsync(id);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> PermaDeleteAsync(Guid id)
        {
            await _repository.PermaDeleteAsync(id);
            return await _repository.SaveChangesAsync();
        }


        public async Task<bool> SaveChangesAsync()
        {
            return await _repository.SaveChangesAsync();
        }
        public async Task<bool> UpdateStatusAsync(Guid id, ScanLentItemDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            var scanTimestamp = DateTime.UtcNow;

            // Update the corresponding Item status based on LentItems status
            var item = await _itemRepository.GetByIdAsync(entity.ItemId);
            if (item != null)
            {
                // Check if item condition is broken when trying to borrow, approve, or set to pending
                if ((dto.LentItemsStatus == LentItemsStatus.Borrowed || dto.LentItemsStatus == LentItemsStatus.Pending || dto.LentItemsStatus == LentItemsStatus.Approved) &&
                    (item.Condition == ItemCondition.Defective || item.Condition == ItemCondition.NeedRepair))
                {
                    throw new InvalidOperationException($"Item '{item.ItemName}' is in {item.Condition} condition and cannot be lent.");
                }

                // Check if trying to set an active status on an item that's already borrowed or reserved
                // (unless it's already in an active status by this same lent item)
                if ((dto.LentItemsStatus == LentItemsStatus.Borrowed || dto.LentItemsStatus == LentItemsStatus.Pending || dto.LentItemsStatus == LentItemsStatus.Approved) && 
                    (item.Status == ItemStatus.Borrowed || item.Status == ItemStatus.Reserved) && 
                    entity.Status != LentItemsStatus.Borrowed.ToString() &&
                    entity.Status != LentItemsStatus.Pending.ToString() &&
                    entity.Status != LentItemsStatus.Approved.ToString())
                {
                    throw new InvalidOperationException($"Item '{item.ItemName}' is already {item.Status.ToString().ToLower()} and cannot be lent.");
                }

                if (dto.LentItemsStatus == LentItemsStatus.Returned)
                {
                    // Set item status back to Available when returned
                    item.Status = ItemStatus.Available;
                    item.UpdatedAt = DateTime.UtcNow;
                    await _itemRepository.UpdateAsync(item);
                }
                else if (dto.LentItemsStatus == LentItemsStatus.Borrowed)
                {
                    // Set item status to Borrowed when borrowed
                    item.Status = ItemStatus.Borrowed;
                    item.UpdatedAt = DateTime.UtcNow;
                    await _itemRepository.UpdateAsync(item);
                }
                // For Approved, set item to Reserved (approved but not yet picked up)
                else if (dto.LentItemsStatus == LentItemsStatus.Approved)
                {
                    item.Status = ItemStatus.Reserved;
                    item.UpdatedAt = DateTime.UtcNow;
                    await _itemRepository.UpdateAsync(item);
                    
                    // Send approval notification when status changes from Pending to Approved
                    if (entity.Status?.Equals("Pending", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        await _notificationService.SendApprovalNotificationAsync(
                            entity.Id,
                            entity.UserId,
                            entity.ItemName ?? item.ItemName,
                            entity.BorrowerFullName ?? "Unknown"
                        );
                    }
                }
                // For Pending, set item to Reserved
                else if (dto.LentItemsStatus == LentItemsStatus.Pending)
                {
                    item.Status = ItemStatus.Reserved;
                    item.UpdatedAt = DateTime.UtcNow;
                    await _itemRepository.UpdateAsync(item);
                }
                // For Canceled, set item back to Available
                else if (dto.LentItemsStatus == LentItemsStatus.Canceled)
                {
                    item.Status = ItemStatus.Available;
                    item.UpdatedAt = DateTime.UtcNow;
                    await _itemRepository.UpdateAsync(item);
                }
                // For Denied, set item back to Available
                else if (dto.LentItemsStatus == LentItemsStatus.Denied)
                {
                    item.Status = ItemStatus.Available;
                    item.UpdatedAt = DateTime.UtcNow;
                    await _itemRepository.UpdateAsync(item);
                }
            }

            var previousStatus = entity.Status;
            entity.Status = dto.LentItemsStatus.ToString();

            // Check the new status to decide which field to update
            if (dto.LentItemsStatus == LentItemsStatus.Returned)
            {
                // Set the ReturnedAt time to the current server UTC time
                entity.ReturnedAt = scanTimestamp;
            }
            else if (dto.LentItemsStatus == LentItemsStatus.Borrowed)
            {
                // Set the LentAt time to the current server UTC time
                entity.LentAt = scanTimestamp;
            }
            else if (dto.LentItemsStatus == LentItemsStatus.Pending || dto.LentItemsStatus == LentItemsStatus.Approved || dto.LentItemsStatus == LentItemsStatus.Canceled || dto.LentItemsStatus == LentItemsStatus.Denied)
            {
                // Clear LentAt when status changes to Pending, Approved, Canceled, or Denied
                entity.LentAt = null;
            }

            await _repository.UpdateAsync(entity);
            var saved = await _repository.SaveChangesAsync();

            // Log the RFID-triggered status transition
            var scanLogCategory = dto.LentItemsStatus switch
            {
                LentItemsStatus.Borrowed => ActivityLogCategory.BorrowedItem,
                LentItemsStatus.Returned => ActivityLogCategory.Returned,
                LentItemsStatus.Approved => ActivityLogCategory.Approved,
                LentItemsStatus.Denied   => ActivityLogCategory.Denied,
                LentItemsStatus.Canceled => ActivityLogCategory.Canceled,
                LentItemsStatus.Reserved => ActivityLogCategory.Reserved,
                _                        => ActivityLogCategory.StatusChange
            };
            await WriteLogAsync(scanLogCategory, $"RFID scan: status set to '{dto.LentItemsStatus}'", entity, previousStatus, dto.LentItemsStatus.ToString());

            return saved;
        }
        public async Task<bool> UpdateHistoryVisibility(Guid lentItemId, Guid userId, bool isHidden)
        {
            // 1. Fetch the LentItems record by ID.
            var lentItem = await _repository.GetByIdAsync(lentItemId);

            if (lentItem == null)
            {
                return false; // Item not found.
            }

            // 2. Authorization check: Ensure the item belongs to the user.
            //    We check both UserId and TeacherId (if a teacher borrowed it for a class, 
            //    they should still be able to hide it from their history).
            if (lentItem.UserId != userId && lentItem.TeacherId != userId)
            {
                // Note: Admin/Staff management access to ALL lent items is handled 
                // separately via the Authorize(Policy = "AdminOrStaff") on the main update endpoints.
                // This specific method is ONLY for a user editing their *own* history, so 
                // we enforce ownership check.
                return false; // Not authorized (item does not belong to this user).
            }

            // 3. Update the visibility flag only if it's changing (to avoid unnecessary save)
            if (lentItem.IsHiddenFromUser == isHidden)
            {
                return true; // Already in the desired state.
            }

            lentItem.IsHiddenFromUser = isHidden;

            // 4. Save the changes to the database.
            await _repository.UpdateAsync(lentItem);
            return await _repository.SaveChangesAsync();
        }

        public async Task<(bool Success, string ErrorMessage)> ArchiveLentItems(Guid id)
        {
            var lentItemsToArchive = await _repository.GetByIdAsync(id); // Uses _repository (ILentItemsRepository)
            if (lentItemsToArchive == null) 
                return (false, "Lent item not found.");

            try
            {
                // Update item status based on whether the lent item was returned
                var item = await _itemRepository.GetByIdAsync(lentItemsToArchive.ItemId);
                if (item != null)
                {
                    // If returned, set back to Available; otherwise set to Archived
                    if (lentItemsToArchive.Status == LentItemsStatus.Returned.ToString())
                    {
                        item.Status = ItemStatus.Available;
                    }
                    else
                    {
                        item.Status = ItemStatus.Archived;
                    }
                    item.UpdatedAt = DateTime.UtcNow;
                    await _itemRepository.UpdateAsync(item);
                }

                var archiveDto = _mapper.Map<CreateArchiveLentItemsDto>(lentItemsToArchive);

                // **OPERATION 1 (Starts here) - Calls ArchiveLentItemsService**
                await _archiveLentItemsService.CreateLentItemsArchiveAsync(archiveDto);
                // This service uses _archiveLentItemsRepository to ADD AND SAVE changes.
                // The DbContext associated with _archiveLentItemsRepository performs a SAVE.

                // **OPERATION 2 (Starts here) - Calls LentItemsRepository**
                await _repository.PermaDeleteAsync(id); // Uses _repository (ILentItemsRepository) to MARK FOR DELETION

                // **OPERATION 3 (Finishes Operation 2)**
                var success = await _repository.SaveChangesAsync(); // Uses _repository's DbContext to SAVE.
                
                return success 
                    ? (true, string.Empty) 
                    : (false, "Failed to save changes during archiving process.");
            }
            catch (Exception ex)
            {
                return (false, $"Archive operation failed: {ex.Message}");
            }
        }

        // Helper: write one activity log entry without blocking the main flow
        private async Task WriteLogAsync(
            ActivityLogCategory category,
            string action,
            LentItems lentItem,
            string? previousStatus,
            string? newStatus)
        {
            try
            {
                var item = lentItem.Item ?? (lentItem.ItemId != Guid.Empty
                    ? await _itemRepository.GetByIdAsync(lentItem.ItemId)
                    : null);

                await _activityLogService.LogAsync(
                    category: category,
                    action: action,
                    actorUserId: lentItem.UserId ?? lentItem.TeacherId,
                    actorName: lentItem.BorrowerFullName,
                    actorRole: lentItem.BorrowerRole,
                    itemId: lentItem.ItemId == Guid.Empty ? null : lentItem.ItemId,
                    itemName: lentItem.ItemName,
                    itemSerialNumber: item?.SerialNumber,
                    lentItemId: lentItem.Id,
                    previousStatus: previousStatus,
                    newStatus: newStatus,
                    borrowedAt: lentItem.LentAt,
                    returnedAt: lentItem.ReturnedAt,
                    reservedFor: lentItem.ReservedFor,
                    remarks: lentItem.Remarks);
            }
            catch
            {
                // Logging must never break the main transaction
            }
        }

        // Validation: Check if item is already reserved for the requested time slot.
        // Simple rule: an item can only have one active reservation at a time.
        // The active-record guard in AddReservationAsync already enforces this;
        // this helper is kept for the guest path which has its own guard.
        private async Task<bool> IsItemAvailableForReservation(
            Guid itemId,
            DateTime? reservedFor,
            IEnumerable<LentItems>? allLentItems = null,
            Guid? excludeLentItemId = null)
        {
            if (!reservedFor.HasValue)
                return true;

            if (allLentItems == null)
                allLentItems = await _repository.GetAllAsync();

            var conflict = allLentItems.FirstOrDefault(li =>
                li.ItemId == itemId &&
                li.Id != excludeLentItemId &&
                (li.Status == "Pending" || li.Status == "Approved" || li.Status == "Borrowed"));

            return conflict == null;
        }

        // Auto-expiry: Expire reservations that weren't picked up
        public async Task<int> CancelExpiredReservationsAsync()
        {
            var now = DateTime.UtcNow;
            var graceMinutes = 30;
            var cutoff = now.AddMinutes(-graceMinutes);

            // Only load the expired reservations — not the entire table
            var expiredReservations = (await _repository.GetExpiredReservationsAsync(cutoff)).ToList();

            int expiredCount = 0;
            foreach (var reservation in expiredReservations)
            {
                var oldStatus = reservation.Status;
                
                // Update status to Expired
                reservation.Status = LentItemsStatus.Expired.ToString();
                reservation.UpdatedAt = DateTime.UtcNow;

                // Set item back to Available
                var item = await _itemRepository.GetByIdAsync(reservation.ItemId);
                if (item != null)
                {
                    item.Status = ItemStatus.Available;
                    item.UpdatedAt = DateTime.UtcNow;
                    await _itemRepository.UpdateAsync(item);
                }

                await _repository.UpdateAsync(reservation);
                
                // Send notification to the user about the expired reservation
                await _notificationService.SendReservationExpiredNotificationAsync(
                    reservation.Id,
                    reservation.UserId,
                    reservation.ItemName ?? "Unknown Item",
                    reservation.BorrowerFullName ?? "Unknown Borrower",
                    reservation.ReservedFor!.Value
                );

                // Log the expiry
                await WriteLogAsync(
                    ActivityLogCategory.Expired,
                    $"Reservation expired - not picked up within {graceMinutes} minute(s) of scheduled time ({reservation.ReservedFor.Value:yyyy-MM-dd HH:mm} UTC)",
                    reservation,
                    oldStatus,
                    LentItemsStatus.Expired.ToString()
                );

                expiredCount++;
            }

            if (expiredCount > 0)
            {
                await _repository.SaveChangesAsync();
            }

            return expiredCount;
        }

        /// <summary>
        /// Allows a student to cancel their own Pending or Approved reservation.
        /// Cannot cancel if already Borrowed, Returned, Denied, Expired, or Canceled.
        /// </summary>
        public async Task<(bool Success, string ErrorMessage)> CancelReservationAsync(Guid lentItemId, Guid userId)
        {
            // 1. Get the lent item
            var lentItem = await _repository.GetByIdAsync(lentItemId);
            if (lentItem == null)
            {
                return (false, "Reservation not found.");
            }

            // 2. Verify ownership - user can only cancel their own reservations
            if (lentItem.UserId != userId)
            {
                return (false, "You are not authorized to cancel this reservation.");
            }

            // 3. Check if the status allows cancellation
            var currentStatus = lentItem.Status ?? string.Empty;
            var allowedStatuses = new[] { "Pending", "Approved" };
            
            if (!allowedStatuses.Contains(currentStatus, StringComparer.OrdinalIgnoreCase))
            {
                return (false, $"Cannot cancel reservation with status '{currentStatus}'. Only Pending or Approved reservations can be canceled.");
            }

            // 4. Update the lent item status to Canceled
            var oldStatus = lentItem.Status;
            lentItem.Status = LentItemsStatus.Canceled.ToString();
            lentItem.UpdatedAt = DateTime.UtcNow;
            lentItem.LentAt = null;

            // 5. Set the item back to Available
            var item = await _itemRepository.GetByIdAsync(lentItem.ItemId);
            if (item != null)
            {
                item.Status = ItemStatus.Available;
                item.UpdatedAt = DateTime.UtcNow;
                await _itemRepository.UpdateAsync(item);
            }

            // 6. Save changes
            await _repository.UpdateAsync(lentItem);
            var saved = await _repository.SaveChangesAsync();

            if (!saved)
            {
                return (false, "Failed to cancel reservation.");
            }

            // 7. Send notification about the cancellation
            await _notificationService.SendStatusChangeNotificationAsync(
                lentItem.Id,
                lentItem.UserId,
                lentItem.ItemName ?? "Unknown Item",
                oldStatus ?? "Unknown",
                LentItemsStatus.Canceled.ToString()
            );

            // 8. Log the cancellation
            await WriteLogAsync(
                ActivityLogCategory.Canceled,
                $"Reservation canceled by user",
                lentItem,
                oldStatus,
                LentItemsStatus.Canceled.ToString()
            );

            return (true, string.Empty);
        }
    }
}
