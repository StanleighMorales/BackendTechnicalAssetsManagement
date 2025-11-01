using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs;
using BackendTechnicalAssetsManagement.src.DTOs.Archive.Items;
using BackendTechnicalAssetsManagement.src.DTOs.Archive.LentItems;
using BackendTechnicalAssetsManagement.src.DTOs.LentItems;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Repository;
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
        private readonly IMapper _mapper;

        public LentItemsService(ILentItemsRepository repository, IMapper mapper, IUserRepository userRepository, IItemRepository itemRepository, IArchiveLentItemsService archiveLentItemsService)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _itemRepository = itemRepository;
            _archiveLentItemsService = archiveLentItemsService;
        }

        // Create
        public async Task<LentItemsDto> AddAsync(CreateLentItemDto dto)
        {
            var lentItem = _mapper.Map<LentItems>(dto);
            if (dto.ItemId != Guid.Empty)
            {
                var item = await _itemRepository.GetByIdAsync(dto.ItemId);
                if (item != null)
                {
                    // Check if item is already unavailable
                    if (item.Status == ItemStatus.Unavailable)
                    {
                        throw new InvalidOperationException($"Item '{item.ItemName}' is already unavailable and cannot be lent.");
                    }

                    // Check if there's already an active lent item for this item
                    var existingLentItems = await _repository.GetAllAsync();
                    var activeLentItem = existingLentItems.FirstOrDefault(li => 
                        li.ItemId == dto.ItemId && 
                        (li.Status == "Pending" || li.Status == "Borrowed"));
                    
                    if (activeLentItem != null)
                    {
                        throw new InvalidOperationException($"Item '{item.ItemName}' already has an active lent record (Status: {activeLentItem.Status}).");
                    }

                    lentItem.ItemName = item.ItemName;
                    
                    // Set item status to Unavailable for both Pending and Borrowed status
                    if (dto.Status?.Equals("Borrowed", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        item.Status = ItemStatus.Unavailable;
                        item.UpdatedAt = DateTime.UtcNow;
                        lentItem.LentAt = DateTime.UtcNow;
                        await _itemRepository.UpdateAsync(item);
                    }
                    else if (dto.Status?.Equals("Pending", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        item.Status = ItemStatus.Unavailable;
                        item.UpdatedAt = DateTime.UtcNow;
                        // Don't set LentAt for Pending status
                        await _itemRepository.UpdateAsync(item);
                    }
                }
                else
                {

                    throw new KeyNotFoundException($"Item with ID {dto.ItemId} not found.");
                }
            }
            if (dto.UserId.HasValue)
            {
                // You need a method in your user repository to get a user by ID.
                var user = await _userRepository.GetByIdAsync(dto.UserId.Value);
                if (user != null)
                {
                    lentItem.BorrowerFullName = $"{user.FirstName} {user.LastName}";
                    lentItem.BorrowerRole = user.UserRole.ToString();

                    if (user is Student student)
                    {
                        lentItem.StudentIdNumber = student.StudentIdNumber;
                    }
                }
                else
                {
                    // Handle case where UserId is provided but not found.
                    // You could throw an exception or handle it as a validation error.
                    throw new KeyNotFoundException($"User with ID {dto.UserId.Value} not found.");
                }
            }
            if (dto.TeacherId.HasValue)
            {
                // We need to load the Teacher object so AutoMapper can find the name.
                // Assuming your repository can fetch a teacher specifically.
                // You may need to create a `GetTeacherByIdAsync` or similar method.
                var teacher = await _userRepository.GetByIdAsync(dto.TeacherId.Value) as Teacher;
                if (teacher != null)
                {
                    // This ensures the navigation property is loaded for the subsequent mapping.
                    lentItem.Teacher = teacher;
                    lentItem.TeacherFullName = $"{teacher.FirstName} {teacher.LastName}";
                }
                else
                {
                    throw new KeyNotFoundException($"Teacher with ID {dto.TeacherId.Value} not found.");
                }
            }
            // Generate the new barcode before saving
            string barcodeText = await BarcodeGenerator.GenerateLentItemBarcode(_repository.GetDbContext());
            byte[]? barcodeImageBytes = BarcodeImageUtil.GenerateBarcodeImageBytes(barcodeText);

            // Set the barcode information
            lentItem.Barcode = barcodeText;
            lentItem.BarcodeImage = barcodeImageBytes;

            await _repository.AddAsync(lentItem);
            await _repository.SaveChangesAsync(); // This saves the item with the barcode

            // 5. Map the fully created and updated entity to the DTO and return it
            return _mapper.Map<LentItemsDto>(lentItem);
        }
        // In Services/LentItemsService.cs

        public async Task<LentItemsDto> AddForGuestAsync(CreateLentItemsForGuestDto dto)
        {
            // 1. This line maps the properties with matching names from the DTO 
            //    (e.g., ItemId, Room, SubjectTimeSchedule).
            //    At this point, lentItem.BorrowerFullName and lentItem.BorrowerRole are still string.Empty.
            var lentItem = _mapper.Map<LentItems>(dto);

            // 2. *** THIS IS THE CRUCIAL PART THAT WAS LIKELY MISSING ***
            //    We now manually populate the fields that AutoMapper couldn't figure out.
            //    This overwrites the empty default values.
            lentItem.BorrowerFullName = $"{dto.BorrowerFirstName} {dto.BorrowerLastName}";
            if (dto.BorrowerRole != null)
            {
                lentItem.BorrowerRole = dto.BorrowerRole;
            }
            lentItem.TeacherFullName = $"{dto.TeacherFirstName} {dto.TeacherLastName}";

            // 3. Set User and Teacher IDs to null for a "guest"
            lentItem.UserId = null;
            lentItem.TeacherId = null;

            if (dto.BorrowerRole != null && dto.BorrowerRole.Equals("Student", StringComparison.OrdinalIgnoreCase))
            {
                lentItem.StudentIdNumber = dto.StudentIdNumber;
            }

            // Update the corresponding item status based on lent item status
            if (dto.ItemId != Guid.Empty)
            {
                var item = await _itemRepository.GetByIdAsync(dto.ItemId);
                if (item != null)
                {
                    // Check if item is already unavailable
                    if (item.Status == ItemStatus.Unavailable)
                    {
                        throw new InvalidOperationException($"Item '{item.ItemName}' is already unavailable and cannot be lent.");
                    }

                    // Check if there's already an active lent item for this item
                    var existingLentItems = await _repository.GetAllAsync();
                    var activeLentItem = existingLentItems.FirstOrDefault(li => 
                        li.ItemId == dto.ItemId && 
                        (li.Status == "Pending" || li.Status == "Borrowed"));
                    
                    if (activeLentItem != null)
                    {
                        throw new InvalidOperationException($"Item '{item.ItemName}' already has an active lent record (Status: {activeLentItem.Status}).");
                    }

                    lentItem.ItemName = item.ItemName;
                    
                    // Set item status to Unavailable for both Pending and Borrowed status
                    if (dto.Status?.Equals("Borrowed", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        item.Status = ItemStatus.Unavailable;
                        item.UpdatedAt = DateTime.UtcNow;
                        lentItem.LentAt = DateTime.UtcNow;
                        await _itemRepository.UpdateAsync(item);
                    }
                    else if (dto.Status?.Equals("Pending", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        item.Status = ItemStatus.Unavailable;
                        item.UpdatedAt = DateTime.UtcNow;
                        // Don't set LentAt for Pending status
                        await _itemRepository.UpdateAsync(item);
                    }
                }
            }

            // 4. Generate the new barcode before saving
            string barcodeText = await BarcodeGenerator.GenerateLentItemBarcode(_repository.GetDbContext());
            byte[]? barcodeImageBytes = BarcodeImageUtil.GenerateBarcodeImageBytes(barcodeText);

            // Set the barcode information
            lentItem.Barcode = barcodeText;
            lentItem.BarcodeImage = barcodeImageBytes;

            // 5. Add the fully-populated object to the repository and save.
            await _repository.AddAsync(lentItem);
            await _repository.SaveChangesAsync(); // This saves the item with the barcode

            var createdItem = await _repository.GetByIdAsync(lentItem.Id);
            return _mapper.Map<LentItemsDto>(createdItem);
        }


        // Read
        public async Task<IEnumerable<LentItemsDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
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
                var oldStatus = entity.Status;
                var newStatus = dto.Status;

                // If status is changing, update the corresponding item status
                if (!string.Equals(oldStatus, newStatus, StringComparison.OrdinalIgnoreCase))
                {
                    var item = await _itemRepository.GetByIdAsync(entity.ItemId);
                    if (item != null)
                    {
                        // Check if trying to set an active status on an item that's already unavailable
                        if ((newStatus.Equals("Borrowed", StringComparison.OrdinalIgnoreCase) || 
                             newStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase)) && 
                            item.Status == ItemStatus.Unavailable && 
                            !oldStatus.Equals("Borrowed", StringComparison.OrdinalIgnoreCase) &&
                            !oldStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                        {
                            throw new InvalidOperationException($"Item '{item.ItemName}' is already unavailable and cannot be lent.");
                        }

                        // Update item status based on new lent item status
                        if (newStatus.Equals("Borrowed", StringComparison.OrdinalIgnoreCase))
                        {
                            item.Status = ItemStatus.Unavailable;
                            item.UpdatedAt = DateTime.UtcNow;
                            entity.LentAt = DateTime.UtcNow;
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

                        await _itemRepository.UpdateAsync(item);
                    }
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
                // Check if trying to set an active status on an item that's already unavailable
                // (unless it's already in an active status by this same lent item)
                if ((dto.LentItemsStatus == LentItemsStatus.Borrowed || dto.LentItemsStatus == LentItemsStatus.Pending) && 
                    item.Status == ItemStatus.Unavailable && 
                    entity.Status != LentItemsStatus.Borrowed.ToString() &&
                    entity.Status != LentItemsStatus.Pending.ToString())
                {
                    throw new InvalidOperationException($"Item '{item.ItemName}' is already unavailable and cannot be lent.");
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
                    // Ensure item status is Unavailable when borrowed
                    item.Status = ItemStatus.Unavailable;
                    item.UpdatedAt = DateTime.UtcNow;
                    await _itemRepository.UpdateAsync(item);
                }
                // For Pending, set item to Unavailable (reserved)
                else if (dto.LentItemsStatus == LentItemsStatus.Pending)
                {
                    item.Status = ItemStatus.Unavailable;
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
            }

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
            else if (dto.LentItemsStatus == LentItemsStatus.Pending || dto.LentItemsStatus == LentItemsStatus.Canceled)
            {
                // Clear LentAt when status changes to Pending or Canceled
                entity.LentAt = null;
            }

            await _repository.UpdateAsync(entity);
            return await _repository.SaveChangesAsync();
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

        public async Task<bool> UpdateStatusByBarcodeAsync(string barcode, ScanLentItemDto dto)
        {
            var entity = await _repository.GetByBarcodeAsync(barcode);
            if (entity == null)
            {
                return false;
            }

            // Use the existing UpdateStatusAsync logic with the found entity's ID
            return await UpdateStatusAsync(entity.Id, dto);
        }

        public async Task<bool> ReturnItemByItemBarcodeAsync(string itemBarcode)
        {
            // 1. Find the item by its barcode
            var item = await _itemRepository.GetByBarcodeAsync(itemBarcode);
            if (item == null)
            {
                return false; // Item not found
            }

            // 2. Find the active lent item for this item (not returned yet)
            var allLentItems = await _repository.GetAllAsync();
            var activeLentItem = allLentItems.FirstOrDefault(li => 
                li.ItemId == item.Id && 
                !li.Status.Equals("Returned", StringComparison.OrdinalIgnoreCase));

            if (activeLentItem == null)
            {
                return false; // No active lent item found for this item
            }

            // 3. Update the lent item status to "Returned" and set ReturnedAt timestamp
            activeLentItem.Status = "Returned";
            activeLentItem.ReturnedAt = DateTime.UtcNow;

            // 4. Update the item status to Available
            item.Status = ItemStatus.Available;
            item.UpdatedAt = DateTime.UtcNow;

            // 5. Save the changes
            await _itemRepository.UpdateAsync(item);
            await _repository.UpdateAsync(activeLentItem);
            return await _repository.SaveChangesAsync();
        }

        public async Task<(bool Success, string ErrorMessage)> ArchiveLentItems(Guid id)
        {
            var lentItemsToArchive = await _repository.GetByIdAsync(id); // Uses _repository (ILentItemsRepository)
            if (lentItemsToArchive == null) 
                return (false, "Lent item not found.");

            try
            {
                // If the lent item was not returned, make sure the item status is set back to Available
                // when archiving the lent item record
                if (lentItemsToArchive.Status != LentItemsStatus.Returned.ToString())
                {
                    var item = await _itemRepository.GetByIdAsync(lentItemsToArchive.ItemId);
                    if (item != null)
                    {
                        item.Status = ItemStatus.Available;
                        item.UpdatedAt = DateTime.UtcNow;
                        await _itemRepository.UpdateAsync(item);
                    }
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
    }
}