using BackendTechnicalAssetsManagement.src.Data;
using Microsoft.EntityFrameworkCore;
using BackendTechnicalAssetsManagement.src.Interfaces.IService;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;
using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.Interfaces.IRepository;
using AutoMapper;
using BackendTechnicalAssetsManagement.src.Models;

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

        public Task<UserDto?> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetUserProfileDto> GetUserProfileByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if(user == null)
            {
                throw new Exception("User not found");
            }

            var userProfile = new GetUserProfileDto
            {
                Id = userId,
                Username = user.Username
            };
            return userProfile;
        }

        public Task<bool> UpdateUserAsync(int id, UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
