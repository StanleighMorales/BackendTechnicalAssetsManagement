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
        private readonly IDevelopmentLoggerService _developmentLoggerService;


        public AuthService(AppDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IPasswordHashingService passwordHashingService, IUserRepository userRepository, IMapper mapper, IUserValidationService userValidationService, IWebHostEnvironment env, IDevelopmentLoggerService developmentLoggerService)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _passwordHashingService = passwordHashingService;
            _userRepository = userRepository;
            _userValidationService = userValidationService;
            _mapper = mapper;
            _env = env;
            _developmentLoggerService = developmentLoggerService;
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
            if (string.IsNullOrWhiteSpace(request.Password) ||
                request.Password.Length < 8 ||
                !request.Password.Any(char.IsUpper) ||
                !request.Password.Any(char.IsLower) ||
                !request.Password.Any(char.IsDigit) ||
                !request.Password.Any(ch => "!@#$%^&*()_+-=[]{}|;':\",.<>?/`~".Contains(ch)))
            {
                throw new ArgumentException("Password must be at least 8 characters long, and include uppercase, lowercase, number, and special character.");
            }
            newUser.PasswordHash = _passwordHashingService.HashPassword(request.Password);

            newUser.Status = "Offline";
            await _userRepository.AddAsync( newUser );
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDto>(newUser);
        }

        #region Login/Logout
        //public async Task<UserDto> Login(LoginUserDto loginDto)
        //{
        //    var user = await _userRepository.GetByIdentifierAsync(loginDto.Identifier);

        //    if (user == null || string.IsNullOrEmpty(user.PasswordHash))
        //    {
        //        throw new Exception("Invalid username or password.");
        //    }

        //    if (!_passwordHashingService.VerifyPassword(loginDto.Password, user.PasswordHash))
        //    {
        //        throw new Exception("Invalid username or password.");
        //    }

        //    // Revoke any existing refresh tokens for security
        //    var existingTokens = await _context.RefreshTokens
        //                                    .Where(rt => rt.UserId == user.Id && !rt.IsRevoked)
        //                                    .ToListAsync();
        //    foreach (var token in existingTokens)
        //    {
        //        token.IsRevoked = true;
        //        token.RevokedAt = DateTime.UtcNow;
        //    }

        //    string accessToken = CreateAccessToken(user);
        //    var refreshTokenEntity = GenerateRefreshToken();
        //    refreshTokenEntity.UserId = user.Id;

        //    // Store the refresh token in the database only (server-side)
        //    await _context.RefreshTokens.AddAsync(refreshTokenEntity);

        //    // Set ONLY the access token cookie
        //    SetAccessTokenCookie(accessToken);
        //    //_developmentLoggerService.LogTokenCountdown(TimeSpan.FromMinutes(15), "ACCESS");
        //    if (_env.IsDevelopment())
        //    {
        //        // Token is created with 15 minutes expiry. Cookie is set with 5 minutes expiry.
        //        // We log the *actual* token expiry time (15m) for the warning.
        //        _developmentLoggerService.LogTokenSent(TimeSpan.FromMinutes(15), "ACCESS");
        //    }



        //    user.Status = "Online";
        //    await _context.SaveChangesAsync(); // Save both user and new token.

        //    return _mapper.Map<UserDto>(user);
        //}
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

            // Revoke any existing refresh tokens for security (common to both)
            var existingTokens = await _context.RefreshTokens
                                            .Where(rt => rt.UserId == user.Id && !rt.IsRevoked)
                                            .ToListAsync();
            foreach (var token in existingTokens)
            {
                token.IsRevoked = true;
                token.RevokedAt = DateTime.UtcNow;
            }

            // --- SHARED LOGIC ---
            var (accessToken, refreshTokenEntity) = GenerateAndPersistTokens(user);

            // --- WEB-SPECIFIC ACTION: Set Cookie ---
            SetAccessTokenCookie(accessToken);
            if (_env.IsDevelopment())
            {
                _developmentLoggerService.LogTokenSent(TimeSpan.FromMinutes(15), "ACCESS");
            }

            user.Status = "Online";
            await _context.SaveChangesAsync(); // Save user, revoked tokens, and new refresh token.

            return _mapper.Map<UserDto>(user);
        }

        public async Task<MobileLoginResponseDto> LoginMobile(LoginUserDto loginDto)
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

            // Revoke any existing refresh tokens for security (common to both)
            var existingTokens = await _context.RefreshTokens
                                            .Where(rt => rt.UserId == user.Id && !rt.IsRevoked)
                                            .ToListAsync();
            foreach (var token in existingTokens)
            {
                token.IsRevoked = true;
                token.RevokedAt = DateTime.UtcNow;
            }

            // --- SHARED LOGIC ---
            var (accessToken, refreshTokenEntity) = GenerateAndPersistTokens(user);

            // --- MOBILE-SPECIFIC ACTION: No cookie, just update status and save ---
            user.Status = "Online";
            await _context.SaveChangesAsync(); // Save user, revoked tokens, and new refresh token.

            if (_env.IsDevelopment())
            {
                _developmentLoggerService.LogTokenSent(TimeSpan.FromMinutes(15), "ACCESS");
            }

            // --- MOBILE-SPECIFIC RESPONSE: Return DTO with tokens in JSON body ---
            return new MobileLoginResponseDto
            {
                User = _mapper.Map<UserDto>(user),
                AccessToken = accessToken,
                RefreshToken = refreshTokenEntity.Token
            };
        }

        public async Task Logout()
        {
            var userId = GetUserIdFromClaims();

            if (userId == Guid.Empty)
            {
                // User is not authenticated via accessToken, nothing to do.
                ClearAccessTokenCookie();
                return;
            }

            // Find the active refresh token for the user and revoke it.
            // We assume the latest unrevoked token is the current one.
            var tokenEntity = await _context.RefreshTokens
                .Include(rt => rt.User)
                .Where(rt => rt.UserId == userId && !rt.IsRevoked)
                .OrderByDescending(rt => rt.CreatedAt)
                .FirstOrDefaultAsync();

            if (tokenEntity != null)
            {
                // Revoke the token
                tokenEntity.IsRevoked = true;
                tokenEntity.RevokedAt = DateTime.UtcNow;

                // Check if the user exists and update their status
                if (tokenEntity.User != null)
                {
                    tokenEntity.User.Status = "Offline";
                }

                // Save changes for both the token and the user status
                await _context.SaveChangesAsync();
            }

            // Clear the access token cookie from the client's browser
            ClearAccessTokenCookie();
        }
        #endregion

        #region Token Generations/Set
        private (string accessToken, RefreshToken refreshTokenEntity) GenerateAndPersistTokens(User user)
        {
            string accessToken = CreateAccessToken(user);
            var refreshTokenEntity = GenerateRefreshToken();

            // 1. Link the Refresh Token to the user
            refreshTokenEntity.UserId = user.Id;

            // 2. Add the refresh token to the database context (will be saved later)
            _context.RefreshTokens.Add(refreshTokenEntity);

            return (accessToken, refreshTokenEntity);
        }

        public async Task<MobileLoginResponseDto> RefreshTokenMobile(string refreshToken)
        {
            // 1. Find the tokenEntity using the 'refreshToken' string passed in
            var tokenEntity = await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            // 2. Perform validity checks
            if (tokenEntity == null || tokenEntity.IsRevoked || tokenEntity.ExpiresAt < DateTime.UtcNow)
            {
                throw new RefreshTokenException("Invalid or expired refresh token.");
            }

            var user = tokenEntity.User;
            if (user == null)
            {
                throw new RefreshTokenException("Associated user not found for refresh token.");
            }

            // --- CORE LOGIC: TOKEN ROTATION ---

            // 3. Revoke the old refresh token (Security: Token Rotation)
            tokenEntity.IsRevoked = true;
            tokenEntity.RevokedAt = DateTime.UtcNow;
            _context.Update(tokenEntity);

            // 4. Generate new tokens
            string newAccessToken = CreateAccessToken(user);
            var newRefreshTokenEntity = GenerateRefreshToken();
            newRefreshTokenEntity.UserId = user.Id;

            // 5. Add the new refresh token to the database
            await _context.RefreshTokens.AddAsync(newRefreshTokenEntity);

            // 6. Save all changes (old token revoked, new token added)
            await _context.SaveChangesAsync();

            // --- END CORE LOGIC ---

            // 7. Return the new tokens in the DTO
            return new MobileLoginResponseDto
            {
                User = _mapper.Map<UserDto>(user),
                AccessToken = newAccessToken,
                RefreshToken = newRefreshTokenEntity.Token
            };
        }


        public async Task<string> RefreshToken()
        {
            // 1. Get UserId from the current (possibly expired but containing claims) access token
            var userId = GetUserIdFromClaims();

            if (userId == Guid.Empty)
            {
                // This means the access token is missing, structurally invalid, or the claims are unreadable.
                throw new RefreshTokenException("User not authenticated or access token invalid.");
            }

            // 2. Find the latest unrevoked refresh token for this user in the DB
            // We include the User entity to use it later without another DB call
            var tokenEntity = await _context.RefreshTokens
                .Include(rt => rt.User)
                .Where(rt => rt.UserId == userId && !rt.IsRevoked)
                .OrderByDescending(rt => rt.CreatedAt)
                .FirstOrDefaultAsync();

            // 3. Check token validity
            if (tokenEntity == null || tokenEntity.ExpiresAt < DateTime.UtcNow)
            {
                // If the token is missing or expired, the session is over.
                throw new RefreshTokenException("Invalid or expired session. Please log in again.");
            }

            var user = tokenEntity.User;
            if (user == null)
            {
                // Should not happen if foreign key is correctly set, but good defensive programming.
                throw new RefreshTokenException("Associated user not found for refresh token.");
            }

            // --- TOKEN ROTATION & RENEWAL ---

            // 4. Revoke the old refresh token (Security: Token Rotation)
            tokenEntity.IsRevoked = true;
            tokenEntity.RevokedAt = DateTime.UtcNow;
            _context.Update(tokenEntity);

            string newAccessToken = CreateAccessToken(user);
            var newRefreshTokenEntity = GenerateRefreshToken();
            newRefreshTokenEntity.UserId = user.Id;

            // 6. Add the new refresh token to the database
            await _context.RefreshTokens.AddAsync(newRefreshTokenEntity);

            // 7. Save both changes (only new token is saved now)
            await _context.SaveChangesAsync();

            // 8. Set the new access token cookie
            SetAccessTokenCookie(newAccessToken);
            //_developmentLoggerService.LogTokenCountdown(TimeSpan.FromMinutes(5), "ACCESS");
            if (_env.IsDevelopment())
            {
                _developmentLoggerService.LogTokenSent(TimeSpan.FromMinutes(15), "ACCESS");
            }

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
                //expires: DateTime.UtcNow.AddSeconds(30), // <-- Set to 30 seconds for dev test
                expires: DateTime.UtcNow.AddMinutes(15), // Access Token expiry time

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
                ExpiresAt = DateTime.UtcNow.AddDays(7), // You might want to make this configurable
                CreatedAt = DateTime.UtcNow
            };
        }

        private void SetAccessTokenCookie(string accessToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return;

            var isDevelopment = _env.IsDevelopment();

            var accessTokenCookieOptions = new CookieOptions
            {
                HttpOnly = !isDevelopment, // Keep HttpOnly logic based on environment
                //Secure = true,             // Ensure this is true in production
                //SameSite = SameSiteMode.None,
                Secure = false,
                SameSite = SameSiteMode.Lax, // <-- Set to Lax for dev test
                //Expires = DateTime.UtcNow.AddSeconds(30), // <-- Set to 30 seconds for dev 
                Expires = DateTime.UtcNow.AddMinutes(15) // Access Token expiry time

            };

            httpContext.Response.Cookies.Append("accessToken", accessToken, accessTokenCookieOptions);
        }

        private void ClearAccessTokenCookie()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return;

            // To clear a cookie, you instruct the browser to delete it.
            // The default options are usually enough to delete, but specifying the path/domain/samesite might be needed if they were used when setting.
            // Using Delete with no options will generally work for simple cookies.
            httpContext.Response.Cookies.Delete("accessToken");
        }

        /// <summary>
        /// Extracts the UserId from the claims of the currently authenticated user (from the accessToken).
        /// </summary>
        /// <returns>The UserId Guid, or Guid.Empty if not found.</returns>
        private Guid GetUserIdFromClaims()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return Guid.Empty;

            var userIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }

            return Guid.Empty;
        }
        #endregion



    }
}
