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
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IDevelopmentLoggerService _developmentLoggerService;


        public AuthService(AppDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IPasswordHashingService passwordHashingService, IUserRepository userRepository, IMapper mapper, IUserValidationService userValidationService, IWebHostEnvironment env, IDevelopmentLoggerService developmentLoggerService, IRefreshTokenRepository refreshTokenRepository)
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
            _refreshTokenRepository = refreshTokenRepository;
        }

        //public async Task<UserDto> Register(RegisterUserDto request)
        //{
        //    await _userValidationService.ValidateUniqueUserAsync(
        //        request.Username,
        //        request.Email,
        //        request.PhoneNumber
        //        );

        //    User newUser;

        //    switch (request.Role)
        //    {
        //        case UserRole.Student:
        //            newUser = _mapper.Map<Student>(request);
        //            break;
        //        case UserRole.Teacher:
        //            newUser = _mapper.Map<Teacher>(request);
        //            break;
        //        case UserRole.Staff:
        //            newUser = _mapper.Map<Staff>(request);
        //            break;
        //        case UserRole.Admin:
        //            newUser = _mapper.Map<Admin>(request);
        //            break;
        //        default:
        //            // Handle cases where the role is not supported or invalid
        //            throw new ArgumentException("Invalid user role specified.");
        //    }
        //    if (string.IsNullOrWhiteSpace(request.Password) ||
        //        request.Password.Length < 8 ||
        //        !request.Password.Any(char.IsUpper) ||
        //        !request.Password.Any(char.IsLower) ||
        //        !request.Password.Any(char.IsDigit) ||
        //        !request.Password.Any(ch => "!@#$%^&*()_+-=[]{}|;':\",.<>?/`~".Contains(ch)))
        //    {
        //        throw new ArgumentException("Password must be at least 8 characters long, and include uppercase, lowercase, number, and special character.");
        //    }
        //    newUser.PasswordHash = _passwordHashingService.HashPassword(request.Password);

        //    newUser.Status = "Offline";
        //    await _userRepository.AddAsync( newUser );
        //    await _userRepository.SaveChangesAsync();

        //    return _mapper.Map<UserDto>(newUser);
        //}
        public async Task<UserDto> Register(RegisterUserDto request)
        {
            await _userValidationService.ValidateUniqueUserAsync(
                request.Username,
                request.Email,
                request.PhoneNumber ?? string.Empty
                );

            User newUser;

            // FIX: Manually instantiate the correct derived class (no mapping yet)
            switch (request.Role)
            {
                case UserRole.Student:
                    newUser = new Student(); // <--- MANUALLY INSTANTIATE
                    break;
                case UserRole.Teacher:
                    newUser = new Teacher(); // <--- MANUALLY INSTANTIATE
                    break;
                case UserRole.Staff:
                    newUser = new Staff(); // <--- MANUALLY INSTANTIATE
                    break;
                case UserRole.Admin:
                case UserRole.SuperAdmin:
                    // Admin and SuperAdmin are now just the base User class
                    newUser = new User();
                    break;
                default:
                    // Handle cases where the role is not supported or invalid
                    throw new ArgumentException("Invalid user role specified.");
            }

            // NEW: Use AutoMapper to map the DTO *over* the instantiated object.
            // This is the map-over signature: _mapper.Map(source, destination)
            // This uses the correct map (RegisterStudentDto to Student) but doesn't cause the cast error.
            _mapper.Map(request, newUser); // <--- THIS REPLACES THE PROBLEM LINES

            if (!string.IsNullOrEmpty(newUser.PhoneNumber) && newUser.PhoneNumber.Length == 10)
            {
                newUser.PhoneNumber = "0" + newUser.PhoneNumber;
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

            // The rest of the logic remains the same
            newUser.PasswordHash = _passwordHashingService.HashPassword(request.Password);

            newUser.Status = "Offline";
            await _userRepository.AddAsync(newUser);
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

            // Revoke any existing refresh tokens for security (using repository)
            await _refreshTokenRepository.RevokeAllForUserAsync(user.Id);

            // --- SHARED LOGIC ---
            var (accessToken, refreshTokenEntity) = await GenerateAndPersistTokensAsync(user);

            // --- WEB-SPECIFIC ACTION: Set Cookie ---
            SetAccessTokenCookie(accessToken);
            SetRefreshTokenCookie(refreshTokenEntity.Token, refreshTokenEntity.ExpiresAt);
            if (_env.IsDevelopment())
            {
                _developmentLoggerService.LogTokenSent(TimeSpan.FromMinutes(15), "ACCESS");
            }

            user.Status = "Online";
            await _refreshTokenRepository.SaveChangesAsync(); // Save user, revoked tokens, and new refresh token.

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

            // Revoke any existing refresh tokens for security (using repository)
            await _refreshTokenRepository.RevokeAllForUserAsync(user.Id);

            // --- SHARED LOGIC ---
            var (accessToken, refreshTokenEntity) = await GenerateAndPersistTokensAsync(user);

            // --- MOBILE-SPECIFIC ACTION: No cookie, just update status and save ---
            user.Status = "Online";
            await _refreshTokenRepository.SaveChangesAsync(); // Save user, revoked tokens, and new refresh token.

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
                ClearRefreshTokenCookie();
                return;
            }

            // Find the active refresh token for the user and revoke it using repository
            var tokenEntity = await _refreshTokenRepository.GetLatestActiveTokenForUserAsync(userId);

            if (tokenEntity != null)
            {
                // Revoke the token in the DB
                tokenEntity.IsRevoked = true;
                tokenEntity.RevokedAt = DateTime.UtcNow;

                if (tokenEntity.User != null)
                {
                    tokenEntity.User.Status = "Offline";
                }

                await _refreshTokenRepository.SaveChangesAsync();
            }

            // Clear the access token cookie from the client's browser
            ClearAccessTokenCookie();
            // **CHANGE: Clear the Refresh Token Cookie**
            ClearRefreshTokenCookie();
        }
        #endregion

        #region Change-Password
        /// <summary>
        /// Handles password changes for both a user's own account and other users' accounts by administrators.
        /// Users can change their own passwords. Only Admin and SuperAdmin roles can change other users' passwords.
        /// Staff members can only change their own passwords.
        /// </summary>
        /// <param name="userId">The ID of the user whose password will be changed.</param>
        /// <param name="request">A DTO containing the new password and confirmation.</param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown if the user is not authenticated, if they lack the required admin role to change another
        /// user's password, or if they attempt to modify a user with higher privileges (e.g., an Admin changing a SuperAdmin's password).
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the specified target user's ID does not correspond to an existing user in the database.
        /// </exception>
        public async Task ChangePassword(Guid userId, ChangePasswordDto request)
        {
            // The ClaimsPrincipal represents the authenticated user's identity, derived from their access token.
            var currentUserPrincipal = _httpContextAccessor.HttpContext?.User;
            if (currentUserPrincipal == null)
                throw new UnauthorizedAccessException("User context not available");
            var currentUserIdClaim = currentUserPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // A valid Guid must be present in the token; otherwise, the request is unauthorized.
            if (!Guid.TryParse(currentUserIdClaim, out var currentUserId))
            {
                throw new UnauthorizedAccessException("User is not authenticated or token is invalid.");
            }

            // The target is the user whose password will be changed, provided via the route parameter.
            Guid targetUserId = userId;

            // A boolean flag to determine if the user is modifying their own account.
            bool isChangingOwnPassword = targetUserId == currentUserId;

            // Authorization is only required if the requester is attempting to modify a different user's account.
            if (!isChangingOwnPassword)
            {
                // This check enforces the business rule that only Admin and SuperAdmin roles can change passwords for other users.
                // Staff members can only change their own passwords.
                if (!currentUserPrincipal.IsInRole("Admin") && !currentUserPrincipal.IsInRole("SuperAdmin"))
                {
                    throw new UnauthorizedAccessException("You do not have permission to change passwords for other users.");
                }
            }

            // Fetches the full user entity from the database that is about to be modified.
            var userToUpdate = await _userRepository.GetByIdAsync(targetUserId);
            if (userToUpdate == null)
            {
                throw new KeyNotFoundException("The target user was not found.");
            }

            // This is an additional security layer to prevent privilege escalation. For example, a standard 'Admin'
            // should not be able to change the password of a 'SuperAdmin'.
            if (userToUpdate.UserRole == UserRole.SuperAdmin && !currentUserPrincipal.IsInRole("SuperAdmin"))
            {
                throw new UnauthorizedAccessException("You do not have permission to change a SuperAdmin's password.");
            }

            // Hashes the new password securely before storing it in the database.
            userToUpdate.PasswordHash = _passwordHashingService.HashPassword(request.NewPassword ?? throw new ArgumentNullException(nameof(request.NewPassword)));

            // Marks the user entity's state as 'Modified'. This tells the DbContext to generate an UPDATE statement.
            await _userRepository.UpdateAsync(userToUpdate);

            // This is a critical security step. It revokes all active refresh tokens for the target user,
            // effectively logging them out of all devices and browser sessions.
            await _refreshTokenRepository.RevokeAllForUserAsync(targetUserId);

            // Atomically commits all tracked changes (the password update and token revocations) to the database.
            await _userRepository.SaveChangesAsync();

            // If the user changed their own password, their current session must also be terminated immediately.
            if (isChangingOwnPassword)
            {
                // This instructs the user's browser to delete the authentication cookies, completing the logout process.
                ClearAccessTokenCookie();
                ClearRefreshTokenCookie();
            }
        }
        #endregion

        #region Token Generations/Set
        private async Task<(string accessToken, RefreshToken refreshTokenEntity)> GenerateAndPersistTokensAsync(User user)
        {
            string accessToken = CreateAccessToken(user);
            var refreshTokenEntity = GenerateRefreshToken();

            // 1. Link the Refresh Token to the user
            refreshTokenEntity.UserId = user.Id;

            // 2. Add the refresh token using repository (will be saved later)
            await _refreshTokenRepository.AddAsync(refreshTokenEntity);

            return (accessToken, refreshTokenEntity);
        }

        public async Task<MobileLoginResponseDto> RefreshTokenMobile(string refreshToken)
        {
            // 1. Find the tokenEntity using the 'refreshToken' string passed in (using repository)
            var tokenEntity = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

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

            // 4. Generate new tokens
            string newAccessToken = CreateAccessToken(user);
            var newRefreshTokenEntity = GenerateRefreshToken();
            newRefreshTokenEntity.UserId = user.Id;

            // 5. Add the new refresh token to the database (using repository)
            await _refreshTokenRepository.AddAsync(newRefreshTokenEntity);

            // 6. Save all changes (old token revoked, new token added)
            await _refreshTokenRepository.SaveChangesAsync();

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
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new RefreshTokenException("HttpContext not available.");
            }

            // 1. Get the Refresh Token string from the HttpOnly cookie
            // The client MUST send this cookie to the /refresh-token endpoint.
            var refreshTokenString = httpContext.Request.Cookies["4CLC-Auth-SRT"];

            if (string.IsNullOrEmpty(refreshTokenString))
            {
                // This is now the entry point for an expired session.
                throw new RefreshTokenException("Refresh token cookie is missing. Please log in again.");
            }

            // 2. Find the token entity in the database (using repository)
            var tokenEntity = await _refreshTokenRepository.GetByTokenAsync(refreshTokenString);

            // 3. Perform Validity Checks
            if (tokenEntity == null)
            {
                // This means the token in the cookie doesn't match a record.
                throw new RefreshTokenException("Invalid refresh token. No matching record found.");
            }
            if (tokenEntity.IsRevoked)
            {
                // **DETECTED A REPLAY ATTACK / SECURITY ISSUE (optional defense)**
                // If a revoked token is used, it often means the token was stolen and used after the user logged out.
                // You might choose to revoke ALL tokens for this user and log it.
                // Clear the cookie for safety
                ClearRefreshTokenCookie();
                throw new RefreshTokenException("Refresh token is revoked. Please log in again.");
            }
            if (tokenEntity.ExpiresAt < DateTime.UtcNow)
            {
                // The long-lived RT has finally expired.
                ClearRefreshTokenCookie(); // Clear the expired cookie
                throw new RefreshTokenException("Expired session. Please log in again.");
            }

            var user = tokenEntity.User;
            if (user == null)
            {
                // Defensive check
                throw new RefreshTokenException("Associated user not found for refresh token.");
            }

            // --- CORE LOGIC: SECURE TOKEN ROTATION ---

            // 4. Revoke the old refresh token (Security: Token Rotation)
            // This ensures a stolen token can only be used ONCE.
            tokenEntity.IsRevoked = true;
            tokenEntity.RevokedAt = DateTime.UtcNow;

            // 5. Generate NEW tokens (AT and RT)
            string newAccessToken = CreateAccessToken(user);
            var newRefreshTokenEntity = GenerateRefreshToken(); // Generates a completely new RT string
            newRefreshTokenEntity.UserId = user.Id;

            // 6. Add the new refresh token to the database (using repository)
            await _refreshTokenRepository.AddAsync(newRefreshTokenEntity);

            // 7. Save all changes (old token revoked, new token added)
            await _refreshTokenRepository.SaveChangesAsync();

            // 8. Set the NEW Access Token cookie AND the NEW Refresh Token cookie
            SetAccessTokenCookie(newAccessToken);
            SetRefreshTokenCookie(newRefreshTokenEntity.Token, newRefreshTokenEntity.ExpiresAt);

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
                Secure = true,             // Required for SameSite=None
                SameSite = SameSiteMode.None, // Required for cross-domain cookies
                Expires = DateTime.UtcNow.AddMinutes(15) // Access Token expiry time
            };

            httpContext.Response.Cookies.Append("4CLC-XSRF-TOKEN", accessToken, accessTokenCookieOptions);
        }

        private void ClearAccessTokenCookie()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return;

            // To clear a cookie, you instruct the browser to delete it.
            // The default options are usually enough to delete, but specifying the path/domain/samesite might be needed if they were used when setting.
            // Using Delete with no options will generally work for simple cookies.
            httpContext.Response.Cookies.Delete("4CLC-XSRF-TOKEN");
        }
        private void ClearRefreshTokenCookie()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return;

            // To clear, you just call Delete
            httpContext.Response.Cookies.Delete("4CLC-Auth-SRT");
        }
        private void SetRefreshTokenCookie(string refreshToken, DateTime expiresAt)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return;

            var isDevelopment = _env.IsDevelopment();

            var refreshTokenCookieOptions = new CookieOptions
            {
                HttpOnly = true,           // Critical: ALWAYS HttpOnly
                Secure = true,             // Required for SameSite=None
                SameSite = SameSiteMode.None, // Required for cross-domain cookies
                Expires = expiresAt        // Use the token's actual expiry time (e.g., 7 days)
            };

            httpContext.Response.Cookies.Append("4CLC-Auth-SRT", refreshToken, refreshTokenCookieOptions);
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

