using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;
using BackendTechnicalAssetsManagement.src.Services;
using Microsoft.AspNetCore.Hosting;
using Moq;
namespace Test
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IWebHostEnvironment> _hostEnvironmentMock;
        private readonly UserService _userService;
        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _hostEnvironmentMock = new Mock<IWebHostEnvironment>();

            // Create an instance of the service, injecting the mocks
            _userService = new UserService(
                _userRepositoryMock.Object,
                _mapperMock.Object,
                _hostEnvironmentMock.Object
            );

        }
        [Fact]
        public async Task GetUserProfileByIdAsync__ShouldReturnUserProfile_WhenUserExists()
        {
            var userId = 1;
            var fakeUser = new User { Id = userId, Username = "testUser" };
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(fakeUser);

            var result = await _userService.GetUserProfileByIdAsync(userId);

            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(fakeUser.Username, result.Username);
        }
        [Fact]
        public async Task GetUserProfileByIdAsync_ShouldThrowException_WhenUserDoesNotExist()
        {
            //Simple steps 
            // Arrange
            // 1. Define an ID for a user that doesn't exist
            var userId = 99;

            // 2. Set up the repository mock to return null (simulating a user not found)
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
                               .ReturnsAsync((User)null);

            // Act & Assert
            // 3. Verify that calling the method throws the expected exception
            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.GetUserProfileByIdAsync(userId));

            // 4. (Optional) You can also assert the exception message is correct
            Assert.Equal("User not found", exception.Message);
        }
    }
}