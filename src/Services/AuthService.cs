using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;
using BackendTechnicalAssetsManagement.src.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.Services
{
    public class AuthService : IAuthService
    {
        //TODO: add a repository for this later instead of communicating directly to db.
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IUserRepository _userRepository;
        private readonly IUserValidationService _userValidationService;
        private readonly IWebHostEnvironment _env;


        public AuthService(AppDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IPasswordHashingService passwordHashingService, IUserRepository userRepository, IMapper mapper, IUserValidationService userValidationService, IWebHostEnvironment env)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _passwordHashingService = passwordHashingService;
            _userRepository = userRepository;
            _userValidationService = userValidationService;
            _mapper = mapper;
            _env = env;
        }

        public async Task<UserDto> Register(RegisterUserDto request)
        {
            await _userValidationService.ValidateUniqueUserAsync(
                request.Username,
                request.Email,
                request.PhoneNumber
                );

            User newUser;

            switch (request.Role)
            {
                case UserRole.Student:
                    newUser = _mapper.Map<Student>(request);
                    break;
                case UserRole.Teacher:
                    newUser = _mapper.Map<Teacher>(request);
                    break;
                case UserRole.Staff:
                    newUser = _mapper.Map<Staff>(request);
                    break;
                case UserRole.Admin:
                    newUser = _mapper.Map<Admin>(request);
                    break;
                default:
                    // Handle cases where the role is not supported or invalid
                    throw new ArgumentException("Invalid user role specified.");
            }
            newUser.PasswordHash = _passwordHashingService.HashPassword(request.Password);

            await _userRepository.AddAsync( newUser );
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDto>(newUser);
        }

        #region Login/Logout
        public async Task<UserDto> Login(LoginUserDto loginDto)
        {
            var user = await _userRepository.GetByIdentifierAsync(loginDto.Identifier);

            if (user == null || string.IsNullOrEmpty(user.PasswordHash))
            {
                throw new Exception("Invalid username or password.");
            }
            if (!_passwordHashingService.VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                throw new Exception("Invalid username or password.");
            }

            // 1. Create both tokens
            string accessToken = CreateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            // 2. Set both tokens in secure HttpOnly cookies
            SetTokenCookies(accessToken, refreshToken);

            // 3. Update the user record in the database with the new refresh token
            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expires;
            user.Status = "Active";

            await _userRepository.SaveChangesAsync();

            // 4. Return the UserDto, NOT the access token
            return _mapper.Map<UserDto>(user);
        }


        public async Task Logout()
        {
            // Your existing Logout logic is good, but let's make it more robust
            // by clearing both cookies.
            var userIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userIdString) && Guid.TryParse(userIdString, out Guid userId))
            {
                var user = await _userRepository.GetByIdAsync(userId); // Use repository
                if (user != null)
                {
                    user.RefreshToken = null;
                    user.TokenCreated = null;
                    user.TokenExpires = null;
                    user.Status = "Inactive";
                    await _userRepository.SaveChangesAsync();
                }
            }

            // Clear the cookies from the browser
            ClearTokenCookies();
        }
        #endregion
        #region Token Generations/Set
        public async Task<string> RefreshToken()
        {
            var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];

            if (refreshToken is null)
            {
                throw new Exception("Refresh token not found.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user is null || user.RefreshToken != refreshToken || user.TokenExpires < DateTime.Now)
            {
                throw new Exception("Invalid or expired refresh token.");
            }

            string newAccessToken = CreateAccessToken(user);
            return newAccessToken;
        }

        private string CreateAccessToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

                new Claim(ClaimTypes.Name, user.Username),
               
                
            };
            if (!string.IsNullOrEmpty(user.Email))
            {
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };
        }


        private void SetTokenCookies(string accessToken, RefreshToken refreshToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return;

            var isDevelopment = _env.IsDevelopment();

            var accessTokenCookieOptions = new CookieOptions
            {
                HttpOnly = !isDevelopment,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(15)
            };
            httpContext.Response.Cookies.Append("accessToken", accessToken, accessTokenCookieOptions);

            var refreshTokenCookieOptions = new CookieOptions
            {
                HttpOnly = !isDevelopment,
                Secure = true, // Ensure this is true in production
                SameSite = SameSiteMode.Strict,
                Expires = refreshToken.Expires
            };
            httpContext.Response.Cookies.Append("refreshToken", refreshToken.Token, refreshTokenCookieOptions);
        }
        private void ClearTokenCookies()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return;

            // To clear a cookie, you instruct the browser to delete it.
            httpContext.Response.Cookies.Delete("accessToken");
            httpContext.Response.Cookies.Delete("refreshToken");
        }
        #endregion



    }
}
