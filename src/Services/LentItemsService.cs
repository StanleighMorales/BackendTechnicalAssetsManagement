using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs;
using BackendTechnicalAssetsManagement.src.DTOs.LentItems;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.Services
{
    public class LentItemsService : ILentItemsService
    {
        private readonly ILentItemsRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LentItemsService(ILentItemsRepository repository, IMapper mapper, IUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        // Create
        public async Task<LentItemsDto> AddAsync(CreateLentItemDto dto)
        {
            var lentItem = _mapper.Map<LentItems>(dto);
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


            await _repository.AddAsync(lentItem);
            await _repository.SaveChangesAsync();

            var createdItem = await _repository.GetByIdAsync(lentItem.Id);
            return _mapper.Map<LentItemsDto>(createdItem);
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
            lentItem.BorrowerRole = dto.BorrowerRole;
            lentItem.TeacherFullName = $"{dto.TeacherFirstName} {dto.TeacherLastName}";

            // 3. Set User and Teacher IDs to null for a "guest"
            lentItem.UserId = null;
            lentItem.TeacherId = null;

            if (dto.BorrowerRole.Equals("Student", StringComparison.OrdinalIgnoreCase))
            {
                lentItem.StudentIdNumber = dto.StudentIdNumber;
            }

            // 4. Add the fully-populated object to the repository and save.
            await _repository.AddAsync(lentItem);
            await _repository.SaveChangesAsync();

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

        public async Task<LentItemsDto?> GetByDateTimeAsync(DateTime dateTime)
        {
            var item = await _repository.GetByDateTime(dateTime);
            return _mapper.Map<LentItemsDto?>(item);
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

            // 3. Apply the DTO properties onto the fetched entity
            // This special overload of AutoMapper is designed for this exact purpose.
            // It will only update the properties that are not null in the DTO.
            _mapper.Map(dto, entity);

            // Note: If you need complex logic (like updating BorrowerFullName when UserId changes),
            // you would add that logic here before saving.

            // 4. Update the entity in the context and save
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
            // Note: No action is needed for Pending or Canceled, 
            // but the status property is still updated above.

            await _repository.UpdateAsync(entity);
            return await _repository.SaveChangesAsync();
        }
    }
}
