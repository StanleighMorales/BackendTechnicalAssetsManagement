using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;
using BackendTechnicalAssetsManagement.src.DTOs.User;
using AutoMapper;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
namespace BackendTechnicalAssetsManagement.src.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;



        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<BaseProfileDto?> GetUserProfileByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                // Return null, the controller will create the 404 response.
                return null;
            }
            // AutoMapper is smart enough to see that 'user' is a Student (or Teacher, etc.)
            // and will use the correct mapping to create a GetStudentProfileDto.
            return _mapper.Map<BaseProfileDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            // This correctly maps a list of User entities to a list of UserDto objects.
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto?>(user);
        }

        public async Task<bool> UpdateUserProfileAsync(Guid id, UpdateUserProfileDto dto)
        {
            // 1. FETCH
            var userToUpdate = await _userRepository.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return false; // Not found
            }

            // 2. APPLY
            // This single line performs the partial update.
            _mapper.Map(dto, userToUpdate);

            // Handle file uploads separately if they exist in the DTO
            // if (dto.ProfilePicture != null) { ... logic to save file ... }

            // 3. SAVE
            await _userRepository.UpdateAsync(userToUpdate);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var userExists = await _userRepository.GetByIdAsync(id) != null;
            if (!userExists)
            {
                return false; // Not found
            }
            await _userRepository.DeleteAsync(id); // Assuming this is your delete method
            return await _userRepository.SaveChangesAsync();
        }

    }
}
