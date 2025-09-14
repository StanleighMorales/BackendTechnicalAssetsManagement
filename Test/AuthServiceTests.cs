using Xunit;
using Moq;
using BackendTechnicalAssetsManagement;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using BackendTechnicalAssetsManagement.src.Services;
using BackendTechnicalAssetsManagement.src.Interfaces.IRepository;
using BackendTechnicalAssetsManagement.src.Interfaces.IService;
using BackendTechnicalAssetsManagement.src.Models;
using BackendTechnicalAssetsManagement.src.DTOs.User;
using System.Threading.Tasks;
using Xunit;
namespace Test
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPasswordHashingService> _passwordHashingServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordHashingServiceMock = new Mock<IPasswordHashingService>();
            _configurationMock = new Mock<IConfiguration>();
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            _authService = new AuthService(
                null,
                _configurationMock.Object,
                _httpContextAccessorMock.Object,
                _passwordHashingServiceMock.Object,
                _userRepositoryMock.Object,
                _mapperMock.Object
                );
        }

        [Fact]
        public async Task Login_ShouldReturnAccessToken_WhenCredintialsAreValid()
        {
            //Arrange Block
            var loginDto = new LoginUserDto { Identifier = "testuser", Password = "password123" };
            var fakeUser = new User { Id = 1, Username = "testuser", PasswordHash = "hashed_password_from_db" };

            _userRepositoryMock.Setup(repo => repo.GetByIdentifierAsync(loginDto.Identifier)).ReturnsAsync(fakeUser);
            _passwordHashingServiceMock.Setup(service => service.VerifyPassword(loginDto.Password, fakeUser.PasswordHash)).Returns(true);

            var configSectionMock = new Mock<IConfigurationSection>();

            configSectionMock.Setup(x => x.Value).Returns("this_is_my_super_secret_key_for_testing_and_it_is_definitely_longer_than_64_characters");
            _configurationMock.Setup(conf => conf.GetSection("AppSettings:Token")).Returns(configSectionMock.Object);

            var httpContext = new DefaultHttpContext();
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            //Act
            var result = await _authService.Login(loginDto);

            //Assert
            Assert.False(string.IsNullOrEmpty(result));
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);

        }
        [Fact]
        public async Task Login_ShouldThrowException_WhenPasswordIsInvalid()
        {
            // Arrange
            var loginDto = new LoginUserDto { Identifier = "testuser", Password = "wrong_password" }; // Note: Changed password
            var fakeUser = new User { Id = 1, Username = "testuser", PasswordHash = "hashed_password_from_db" };

            // This part is the same: the user is found in the repository.
            _userRepositoryMock.Setup(repo => repo.GetByIdentifierAsync(loginDto.Identifier))
                               .ReturnsAsync(fakeUser);

            // --- THIS IS THE CRITICAL CHANGE ---
            // Teach the password hasher mock: for this test, the password is WRONG, so return false.
            _passwordHashingServiceMock.Setup(service => service.VerifyPassword(loginDto.Password, fakeUser.PasswordHash))
                                       .Returns(false);
            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _authService.Login(loginDto));

            Assert.Equal("Invalid username or password.", exception.Message);
        }
        [Fact]
        public async Task Login_ShouldThrowException_WhenUserDoesNotExist()
        {
            //Arrange
            var loginDto = new LoginUserDto { Identifier = "nonExistentUser", Password = "whoKnows" };
            var fakeUser = new User { Id = 1, Username = "testuser", PasswordHash = "hashed_password_form_db" };

            _userRepositoryMock.Setup(repo => repo.GetByIdentifierAsync(loginDto.Identifier)).ReturnsAsync((User)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => _authService.Login(loginDto));

            Assert.Equal("Invalid username or password.", exception.Message);

        }
        //TODO: Logout
        //TODO: RegisterStaff
        //TODO: RefreshToken
        //TODO: Create AccessToken
        //TODO: SetRefreshTokenCookie

    }
}
