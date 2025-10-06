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
        

        public AuthService(AppDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IPasswordHashingService passwordHashingService, IUserRepository userRepository, IMapper mapper, IUserValidationService userValidationService)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _passwordHashingService = passwordHashingService;
            _userRepository = userRepository;
            _userValidationService = userValidationService;
            _mapper = mapper;
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

        //public async Task<UserDto> RegisterStaffAsync(RegisterStaffDto staffDto)
        //{
        //    if (await _userRepository.GetByUsernameAsync(staffDto.Username) != null)
        //    {
        //        // Throw an exception that the controller can catch.
        //        // This prevents creating duplicate usernames.
        //        throw new Exception($"Username '{staffDto.Username}' is already taken.");
        //    }
        //    if (await _userRepository.GetByEmailAsync(staffDto.Email) != null)
        //    {
        //        throw new Exception($"Email '{staffDto.Email}' already exist.");
        //    }
        //    if (await _userRepository.GetByPhoneNumberAsync(staffDto.PhoneNumber) != null)
        //    {
        //        throw new Exception($"Phone Number already used.");

        //    }


        //    var staffModel = _mapper.Map<Staff>(staffDto);

        //    staffModel.PasswordHash = _passwordHashingService.HashPassword(staffDto.Password);

        //    await _userRepository.AddAsync(staffModel);
        //    await _userRepository.SaveChangesAsync();

        //    return _mapper.Map<UserDto>(staffModel);
        //}
        //public async Task<UserDto> RegisterTeacherAsync(RegisterTeacherDto teacherDto)
        //{ 
        //    await _userValidationService.ValidateUniqueUserAsync(
        //        teacherDto.Username,
        //        teacherDto.Email,
        //        teacherDto.PhoneNumber
        //        );

        //    var teacherModel = _mapper.Map<Teacher>(teacherDto);
        //    teacherModel.PasswordHash = _passwordHashingService.HashPassword(teacherDto.Password);

        //    await _userRepository.AddAsync(teacherModel);
        //    await _userRepository.SaveChangesAsync(); 

        //    return _mapper.Map<UserDto>(teacherModel);
        //}
        //public async Task<UserDto> RegisterStudentAsync(RegisterStudentDto studentDto)
        //{
        //    await _userValidationService.ValidateUniqueUserAsync(
        //        studentDto.Username,
        //        studentDto.Email,
        //        studentDto.PhoneNumber
        //        );

        //    var studentModel = _mapper.Map<Student>(studentDto);

        //    ValidateImageUtil.ValidateImage(studentDto.FrontStudentIdPicture);
        //    ValidateImageUtil.ValidateImage(studentDto.BackStudentIdPicture);
        //    ValidateImageUtil.ValidateImage(studentDto.ProfilePicture);

        //    studentModel.PasswordHash = _passwordHashingService.HashPassword(studentDto.Password);

        //    await _userRepository.AddAsync(studentModel);
        //    await _userRepository.SaveChangesAsync();

        //    return _mapper.Map<UserDto>(studentModel);

        //}

        //public async Task<UserDto> RegisterManagerAsync(RegisterManagerDto managerDto)
        //{
        //    await _userValidationService.ValidateUniqueUserAsync(
        //        managerDto.Username,
        //        managerDto.Email,
        //        managerDto.PhoneNumber
        //        );

        //    var managerModel = _mapper.Map<Manager>(managerDto);

        //    managerModel.PasswordHash = _passwordHashingService.HashPassword(managerDto.Password);

        //    await _userRepository.AddAsync(managerModel);
        //    await _userRepository.SaveChangesAsync();

        //    return _mapper.Map<UserDto>(managerModel);
        //}

        //public async Task<UserDto> RegisterAdminAsync(RegisterAdminDto adminDto)
        //{
        //    await _userValidationService.ValidateUniqueUserAsync(
        //        adminDto.Username,
        //        adminDto.Email,
        //        adminDto.PhoneNumber
        //        );
        //    var adminModel = _mapper.Map<Admin>(adminDto);
        //    adminModel.PasswordHash = _passwordHashingService.HashPassword(adminDto.Password);

        //    await _userRepository.AddAsync(adminModel);
        //    await _userRepository.SaveChangesAsync();

        //    return _mapper.Map<UserDto>(adminDto);
        //}
        #region Login/Logout
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
            user.Status = "Active";

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
            user.Status = "Inactive";

            await _context.SaveChangesAsync();

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(-1)
            };
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", "", cookieOptions);
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
        #endregion



    }
}
