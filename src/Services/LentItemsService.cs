using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs;
using BackendTechnicalAssetsManagement.src.DTOs.LentItems;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;

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
        public async Task UpdateAsync(UpdateLentItemDto dto)
        {
            var entity = _mapper.Map<LentItems>(dto);
            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
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

    }
}
