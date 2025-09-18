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

        public Task<bool> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var user = await _userRepository.GetAllAsync();

            return (IEnumerable<UserDto>)_mapper.Map<UserDto>(user);
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
                throw new KeyNotFoundException("User not found");
            }

            return _mapper.Map<BaseProfileDto>(user);
        }

        public Task<bool> UpdateUserAsync(int id, UserDto userDto)
        {
            throw new NotImplementedException();
        }

    }
}
