using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;
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
            // 1. Fetch the user object (it will be the derived type: Student, Teacher, etc.)
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return null; // Not found
            }

            // 2. Explicitly check the runtime type and map to the specific DTO.
            // This circumvents the occasional failure of AutoMapper's runtime polymorphism.
            if (user is Student student)
            {
                return _mapper.Map<GetStudentProfileDto>(student);
            }
            else if (user is Teacher teacher)
            {
                return _mapper.Map<GetTeacherProfileDto>(teacher);
            }
            else if (user is Staff staff)
            {
                return _mapper.Map<GetStaffProfileDto>(staff);
            }

            // Fallback: If the user is a base User or an unknown type, map to the base profile DTO.
            // This is less likely to happen in a TPH setup, but is a safe fallback.
            return _mapper.Map<BaseProfileDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            // This now calls the repository method that returns the fully-formed DTOs.
            // The mapping logic is correctly handled in the repository layer.
            return await _userRepository.GetAllUserDtosAsync();
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
            await _userRepository.DeleteAsync(id); 
            return await _userRepository.SaveChangesAsync();
        }



    }
}
