using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;
using BackendTechnicalAssetsManagement.src.Repository;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;
using static BackendTechnicalAssetsManagement.src.DTOs.User.UserProfileDtos;
namespace BackendTechnicalAssetsManagement.src.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IArchiveUserService _archiveUserService;



        public UserService(IUserRepository userRepository, IMapper mapper, IArchiveUserService archiveUserService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _archiveUserService = archiveUserService;
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

        public async Task<bool> DeleteUserAsync(Guid id, Guid currentUserId)
        {
            
            return await _archiveUserService.ArchiveUserAsync(id, currentUserId);
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync() // Return specific DTO
        {
            // Assuming IUserRepository.GetAllStudentsAsync returns the correct DTO
            return await _userRepository.GetAllStudentsAsync();
        }

        public async Task<IEnumerable<TeacherDto>> GetAllTeachersAsync() // Return specific DTO
        {
            // Assuming IUserRepository.GetAllTeachersAsync returns the correct DTO
            return await _userRepository.GetAllTeachersAsync();
        }

        public async Task<IEnumerable<StaffDto>> GetAllStaffAsync() // Return specific DTO
        {
            // Assuming IUserRepository.GetAllStaffAsync returns the correct DTO
            return await _userRepository.GetAllStaffAsync();
        }

        public async Task UpdateStaffOrAdminProfileAsync(Guid targetUserId, UpdateStaffProfileDto dto, Guid currentUserId)
        {
            // 1. Get the user who is making the request
            var currentUser = await _userRepository.GetByIdAsync(currentUserId);
            // This case implies an issue with the auth token, which is rare.
            // A KeyNotFoundException is appropriate.
            if (currentUser == null)
                throw new KeyNotFoundException("The current user making the request could not be found.");

            // 2. Get the user to be updated
            var userToUpdate = await _userRepository.GetByIdAsync(targetUserId);
            if (userToUpdate == null)
                throw new KeyNotFoundException($"User with ID '{targetUserId}' was not found.");

            // 3. Authorization: Throw a specific exception for permission failure.
            if (!CanUserUpdateProfile(currentUser, userToUpdate))
            {
                // This will be caught by your middleware and turned into a 403 Forbidden.
                throw new UnauthorizedAccessException("You do not have permission to update this user's profile.");
            }

            // 4. Mapping: This logic remains the same.
            if (userToUpdate is Staff staff)
            {
                _mapper.Map(dto, staff);
            }
            else
            {
                _mapper.Map(dto, userToUpdate);
            }

            // 5. Persist Changes: If we reach this point, the operation is successful.
            await _userRepository.UpdateAsync(userToUpdate);
            await _userRepository.SaveChangesAsync();
        }

        // The helper method remains exactly the same, as its logic is still correct.
        private bool CanUserUpdateProfile(User currentUser, User userToUpdate)
        {
            if (currentUser.Id == userToUpdate.Id)
            {
                return true;
            }

            return currentUser.UserRole switch
            {
                UserRole.SuperAdmin => userToUpdate.UserRole is UserRole.Admin or UserRole.Staff,
                UserRole.Admin => userToUpdate.UserRole is UserRole.Staff,
                _ => false
            };
        }
    }
}
