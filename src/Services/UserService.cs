using BackendTechnicalAssetsManagement.src.Interfaces.IService;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;
using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.Interfaces.IRepository;
using AutoMapper;
using static BackendTechnicalAssetsManagement.src.Models.Enums;
namespace BackendTechnicalAssetsManagement.src.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostEnvironment;


        public UserService(IUserRepository userRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _hostEnvironment = webHostEnvironment;


        }

        //public async Task<UserDto> CreateUserAsync(RegisterUserDto registerUserDto)
        //{
        //    var existingUser = await _userRepository.GetByIdAsync(registerUserDto)
        //}

        public Task<bool> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseProfileDto> GetUserProfileByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if(user == null)
            {
                throw new Exception("User not found");
            }

            switch (user.UserRole)
            {
                case UserRole.Student:
                    // Assume you have a _studentRepository to get student-specific info
                    var studentProfile = await _userRepository.GetByIdAsync(userId);
                    return _mapper.Map<GetStudentProfileDto>(studentProfile); // Use AutoMapper for clean mapping

                case UserRole.Teacher:
                    var teacherProfile = await _userRepository.GetByIdAsync(userId);
                    return _mapper.Map<GetTeacherProfileDto>(teacherProfile);

                case UserRole.Staff:
                    var staffProfile = await _userRepository.GetByIdAsync(userId);
                    return _mapper.Map<GetStaffProfileDto>(staffProfile);

                case UserRole.Manager:
                    var managerProfile = await _userRepository.GetByIdAsync(userId);
                    return _mapper.Map<GetManagerProfileDto>(managerProfile);
                case UserRole.Admin:
                    var adminProfile = await _userRepository.GetByIdAsync(userId);
                    return _mapper.Map<GetAdminProfileDto>(adminProfile);

                default:
                    return _mapper.Map<BaseProfileDto>(user);
            }
        }

        public Task<bool> UpdateUserAsync(int id, UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
