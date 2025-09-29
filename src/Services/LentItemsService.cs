using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs;
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
                }
                else
                {
                    throw new KeyNotFoundException($"Teacher with ID {dto.TeacherId.Value} not found.");
                }
            }


            await _repository.AddAsync(lentItem);
            await _repository.SaveChangesAsync();
            return _mapper.Map<LentItemsDto>(lentItem);
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

        // 🔹 Admin-only methods
        public async Task<IEnumerable<LentItemsDto>> GetAllIncludingDeletedAsync()
        {
            var items = await _repository.GetAllIncludingDeletedAsync();
            return _mapper.Map<IEnumerable<LentItemsDto>>(items);
        }

        public async Task<IEnumerable<LentItemsDto>> GetDeletedAsync()
        {
            var items = await _repository.GetDeletedAsync();
            return _mapper.Map<IEnumerable<LentItemsDto>>(items);
        }

        public async Task<LentItemsDto?> GetDeletedByIdAsync(Guid id)
        {
            var item = await _repository.GetDeletedByIdAsync(id);
            return _mapper.Map<LentItemsDto?>(item);
        }

        public async Task<bool> RestoreAsync(Guid id)
        {
            await _repository.RestoreAsync(id);
            return await _repository.SaveChangesAsync();
        }
    }
}
