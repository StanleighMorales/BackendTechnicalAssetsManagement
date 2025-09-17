using AutoMapper;
using BackendTechnicalAssetsManagement.Models;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.Interfaces.IRepository;
using BackendTechnicalAssetsManagement.src.Interfaces.IService;
using BackendTechnicalAssetsManagement.src.Interfaces.IValidations;
using BackendTechnicalAssetsManagement.src.Models;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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
        private readonly IWebHostEnvironment _hostEnvironment;
        

        public AuthService(AppDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IPasswordHashingService passwordHashingService, IUserRepository userRepository, IMapper mapper, IUserValidationService userValidationService, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _passwordHashingService = passwordHashingService;
            _userRepository = userRepository;
            _userValidationService = userValidationService;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<User> Register(RegisterUserDto request)
        {
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            {
                throw new Exception("Username already Exist");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User()
            {
                Username = request.Username,
                PasswordHash = hashedPassword
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<UserDto> RegisterStaffAsync(RegisterStaffDto staffDto)
        {
            if (await _userRepository.GetByUsernameAsync(staffDto.Username) != null)
            {
                // Throw an exception that the controller can catch.
                // This prevents creating duplicate usernames.
                throw new Exception($"Username '{staffDto.Username}' is already taken.");
            }
            if (await _userRepository.GetByEmailAsync(staffDto.Email) != null)
            {
                throw new Exception($"Email '{staffDto.Email}' already exist.");
            }
            if (await _userRepository.GetByPhoneNumberAsync(staffDto.PhoneNumber) != null)
            {
                throw new Exception($"Phone Number already used.");

            }


            var staffModel = _mapper.Map<Staff>(staffDto);

            staffModel.PasswordHash = _passwordHashingService.HashPassword(staffDto.Password);

            await _userRepository.AddAsync(staffModel);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDto>(staffModel);
        }
        public async Task<UserDto> RegisterTeacherAsync(RegisterTeacherDto teacherDto)
        { 
            await _userValidationService.ValidateUniqueUserAsync(
                teacherDto.Username,
                teacherDto.Email,
                teacherDto.PhoneNumber
                );

            var teacherModel = _mapper.Map<Teacher>(teacherDto);
            teacherModel.PasswordHash = _passwordHashingService.HashPassword(teacherDto.Password);

            await _userRepository.AddAsync(teacherModel);
            await _userRepository.SaveChangesAsync(); 

            return _mapper.Map<UserDto>(teacherModel);
        }
        public async Task<UserDto> RegisterStudentAsync(RegisterStudentDto studentDto)
        {
            await _userValidationService.ValidateUniqueUserAsync(
                studentDto.Username,
                studentDto.Email,
                studentDto.PhoneNumber
                );
            string? imagePathFrontId = await SaveImageWithValidationAsync(studentDto.FrontStudentIdPicture);
            string? imagePathBackId = await SaveImageWithValidationAsync(studentDto.BackStudentIdPicture);
            string? imagePathProfile = await SaveImageWithValidationAsync(studentDto.ProfilePicture);

            var studenModel = _mapper.Map<Student>(studentDto);
            studenModel.PasswordHash = _passwordHashingService.HashPassword(studentDto.Password);

            await _userRepository.AddAsync(studenModel);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDto>(studenModel);

        }

        public async Task<UserDto> RegisterManagerAsync(RegisterManagerDto managerDto)
        {
            await _userValidationService.ValidateUniqueUserAsync(
                managerDto.Username,
                managerDto.Email,
                managerDto.PhoneNumber
                );

            var managerModel = _mapper.Map<Manager>(managerDto);

            managerModel.PasswordHash = _passwordHashingService.HashPassword(managerDto.Password);

            await _userRepository.AddAsync(managerModel);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDto>(managerModel);
        }

        public async Task<UserDto> RegisterAdminAsync(RegisterAdminDto adminDto)
        {
            await _userValidationService.ValidateUniqueUserAsync(
                adminDto.Username,
                adminDto.Email,
                adminDto.PhoneNumber
                );
            var adminModel = _mapper.Map<Admin>(adminDto);
            adminModel.PasswordHash = _passwordHashingService.HashPassword(adminDto.Password);

            await _userRepository.AddAsync(adminModel);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDto>(adminDto);
        }

        public async Task<string> Login(LoginUserDto loginDto)
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

            var refreshToken = GenerateRefreshToken();

            SetRefreshTokenCookie(refreshToken);

            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expires;

            await _userRepository.SaveChangesAsync();

            return accessToken;
        }
        public async Task Logout()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                throw new Exception("User ID not found in token");
            }

            var user = await _context.Users.FindAsync(Guid.Parse(userIdString));
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.RefreshToken = null;
            user.TokenCreated = new DateTime();
            user.TokenExpires = new DateTime();

            await _context.SaveChangesAsync();

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(-1)
            };
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", "", cookieOptions);
        }
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

        private void SetRefreshTokenCookie(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = newRefreshToken.Expires
            };
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
        }

        private async Task<string?> SaveImageWithValidationAsync(IFormFile? image)
        {
            if (image == null || image.Length == 0) return null;
            if (image.Length > 2 * 1024 * 1024) throw new ArgumentException("Image file size cannot exceed 2MB");

            var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif", ".webp" };
            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("Invalid image file type.");
            }

            if (string.IsNullOrEmpty(_hostEnvironment.WebRootPath))
            {
                throw new InvalidOperationException("wwwroot folder is not configured.");
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images", "items");
            Directory.CreateDirectory(uploadsFolder);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return Path.Combine("images", "items", uniqueFileName).Replace('\\', '/');
        }


    }
}
