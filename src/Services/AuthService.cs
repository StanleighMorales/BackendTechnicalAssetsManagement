using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.Exceptions;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
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

            string accessToken = CreateAccessToken(user);
            var refreshTokenEntity = GenerateRefreshToken();

            refreshTokenEntity.UserId = user.Id;
            await _context.RefreshTokens.AddAsync(refreshTokenEntity);

            // Your SetTokenCookies method will need to accept the entity.
            SetTokenCookies(accessToken, refreshTokenEntity);

            user.Status = "Active";
            await _context.SaveChangesAsync(); // Save both user and new token.

            return _mapper.Map<UserDto>(user);
        }


        public async Task Logout()
        {
            var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                // If there's no token, there's nothing to do
                return;
            }

            // Include the User entity when fetching the token
            var tokenEntity = await _context.RefreshTokens
                .Include(rt => rt.User) // Eagerly load the related User
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            if (tokenEntity != null)
            {
                // Revoke the token
                tokenEntity.IsRevoked = true;
                tokenEntity.RevokedAt = DateTime.UtcNow;

                // Check if the user exists and update their status
                if (tokenEntity.User != null)
                {
                    tokenEntity.User.Status = "Inactive";
                }

                // Save changes for both the token and the user status
                await _context.SaveChangesAsync();
            }

            // Clear the cookies from the client's browser
            ClearTokenCookies();
        }
        #endregion

        #region Token Generations/Set
        public async Task<string> RefreshToken()
        {
            var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new RefreshTokenException("Refresh token not found.");
            }

            // Find the token in the DB, including the user data
            var tokenEntity = await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            if (tokenEntity == null || tokenEntity.IsRevoked || tokenEntity.ExpiresAt < DateTime.UtcNow)
            {
                throw new RefreshTokenException("Invalid or expired refresh token.");
            }

            // Revoke the old token
            tokenEntity.IsRevoked = true;
            tokenEntity.RevokedAt = DateTime.UtcNow;

            _context.Update(tokenEntity);


            // --- SIMPLIFIED CODE ---
            var user = tokenEntity.User;
            string newAccessToken = CreateAccessToken(user);

            // 1. Generate the new complete RefreshToken entity.
            var newRefreshTokenEntity = GenerateRefreshToken();

            // 2. Assign the UserId to it.
            newRefreshTokenEntity.UserId = user.Id;

            // 3. Add it to the database.
            await _context.RefreshTokens.AddAsync(newRefreshTokenEntity);

            await _context.SaveChangesAsync();

            SetTokenCookies(newAccessToken, newRefreshTokenEntity);

            return newAccessToken;

        }

        private string CreateAccessToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

                new Claim(ClaimTypes.Name, user.Username),

                new Claim(ClaimTypes.Role, user.UserRole.ToString()),


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
                expires: DateTime.UtcNow.AddMinutes(1),
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
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
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
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(15)
            };
            httpContext.Response.Cookies.Append("accessToken", accessToken, accessTokenCookieOptions);

            var refreshTokenCookieOptions = new CookieOptions
            {
                HttpOnly = !isDevelopment,
                Secure = true, // Ensure this is true in production
                SameSite = SameSiteMode.None,
                Expires = refreshToken.ExpiresAt
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
