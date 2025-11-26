using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;
using BackendTechnicalAssetsManagement.src.Repository;
using BackendTechnicalAssetsManagement.src.Utils;
using ExcelDataReader;
using System.Data;
using System.Text;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;
using static BackendTechnicalAssetsManagement.src.DTOs.User.UserProfileDtos;
namespace BackendTechnicalAssetsManagement.src.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IArchiveUserService _archiveUserService;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IExcelReaderService _excelReaderService;

        public UserService(IUserRepository userRepository, IMapper mapper, IArchiveUserService archiveUserService, IPasswordHashingService passwordHashingService, IExcelReaderService excelReaderService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _archiveUserService = archiveUserService;
            _passwordHashingService = passwordHashingService;
            _excelReaderService = excelReaderService;
        }

        public async Task<BaseProfileDto?> GetUserProfileByIdAsync(Guid userId)
        {
            // 1. Fetch the user object (it will be the derived type: Student, Teacher, etc.)
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return null; // Not found
            }

            // 2. Explicitly check the runtime type and map to the specific DTO.
            // This circumvents the occasional failure of AutoMapper's runtime polymorphism.
            if (user is Student student)
            {
                return _mapper.Map<GetStudentProfileDto>(student);
            }
            else if (user is Teacher teacher)
            {
                return _mapper.Map<GetTeacherProfileDto>(teacher);
            }
            else if (user is Staff staff)
            {
                return _mapper.Map<GetStaffProfileDto>(staff);
            }

            // Fallback: If the user is a base User or an unknown type, map to the base profile DTO.
            // This is less likely to happen in a TPH setup, but is a safe fallback.
            return _mapper.Map<BaseProfileDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            // This now calls the repository method that returns the fully-formed DTOs.
            // The mapping logic is correctly handled in the repository layer.
            return await _userRepository.GetAllUserDtosAsync();
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto?>(user);
        }

        public async Task<bool> UpdateUserProfileAsync(Guid id, UpdateUserProfileDto dto)
        {
            // 1. FETCH
            var userToUpdate = await _userRepository.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return false; // Not found
            }

            // 2. APPLY
            // This single line performs the partial update.
            _mapper.Map(dto, userToUpdate);

            // Handle file uploads separately if they exist in the DTO
            // if (dto.ProfilePicture != null) { ... logic to save file ... }

            // 3. SAVE
            await _userRepository.UpdateAsync(userToUpdate);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateStudentProfileAsync(Guid id, UpdateStudentProfileDto dto)
        {
            var userToUpdate = await _userRepository.GetByIdAsync(id);
            if (userToUpdate is not Student studentToUpdate)
            {
                return false; // Not found or not a student
            }

            // Centralized validation
            try
            {
                if (dto.ProfilePicture != null) ImageConverterUtils.ValidateImage(dto.ProfilePicture);
                if (dto.FrontStudentIdPicture != null) ImageConverterUtils.ValidateImage(dto.FrontStudentIdPicture);
                if (dto.BackStudentIdPicture != null) ImageConverterUtils.ValidateImage(dto.BackStudentIdPicture);
            }
            catch (ArgumentException)
            {
                // Re-throw or handle as a custom exception that your middleware can catch
                throw;
            }

            _mapper.Map(dto, studentToUpdate);

            await _userRepository.UpdateAsync(studentToUpdate);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteUserAsync(Guid id, Guid currentUserId)
        {
            
            return await _archiveUserService.ArchiveUserAsync(id, currentUserId);
        }

        public async Task UpdateStaffOrAdminProfileAsync(Guid targetUserId, UpdateStaffProfileDto dto, Guid currentUserId)
        {
            // 1. Get the user who is making the request
            var currentUser = await _userRepository.GetByIdAsync(currentUserId);
            // This case implies an issue with the auth token, which is rare.
            // A KeyNotFoundException is appropriate.
            if (currentUser == null)
                throw new KeyNotFoundException("The current user making the request could not be found.");

            // 2. Get the user to be updated
            var userToUpdate = await _userRepository.GetByIdAsync(targetUserId);
            if (userToUpdate == null)
                throw new KeyNotFoundException($"User with ID '{targetUserId}' was not found.");

            // 3. Authorization: Throw a specific exception for permission failure.
            if (!CanUserUpdateProfile(currentUser, userToUpdate))
            {
                // This will be caught by your middleware and turned into a 403 Forbidden.
                throw new UnauthorizedAccessException("You do not have permission to update this user's profile.");
            }

            // 4. Mapping: This logic remains the same.
            if (userToUpdate is Staff staff)
            {
                _mapper.Map(dto, staff);
            }
            else
            {
                _mapper.Map(dto, userToUpdate);
            }

            // 5. Persist Changes: If we reach this point, the operation is successful.
            await _userRepository.UpdateAsync(userToUpdate);
            await _userRepository.SaveChangesAsync();
        }

        // The helper method remains exactly the same, as its logic is still correct.
        private bool CanUserUpdateProfile(User currentUser, User userToUpdate)
        {
            if (currentUser.Id == userToUpdate.Id)
            {
                return true;
            }

            return currentUser.UserRole switch
            {
                UserRole.SuperAdmin => userToUpdate.UserRole is UserRole.Admin or UserRole.Staff,
                UserRole.Admin => userToUpdate.UserRole is UserRole.Staff,
                _ => false
            };
        }

        /// <summary>
        /// Imports multiple students from Excel file with auto-generated usernames and passwords
        /// Expected columns: LastName, FirstName, MiddleName (optional)
        /// </summary>
        public async Task<ImportStudentsResponseDto> ImportStudentsFromExcelAsync(IFormFile file)
        {
            var response = new ImportStudentsResponseDto();

            // Use the Excel reader service to extract student data
            var (students, errorMessage) = await _excelReaderService.ReadStudentsFromExcelAsync(file);

            if (errorMessage != null)
            {
                response.Errors.Add(errorMessage);
                response.FailureCount = students.Count;
                response.TotalProcessed = students.Count;
                return response;
            }

            response.TotalProcessed = students.Count;

            // Process each student
            foreach (var (firstName, lastName, middleName, rowNumber) in students)
            {
                try
                {
                    // Validate required fields
                    if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                    {
                        response.FailureCount++;
                        response.Errors.Add($"Row {rowNumber}: Missing required fields (FirstName or LastName)");
                        continue;
                    }

                    // Check if student with same name already exists
                    var allUsers = await _userRepository.GetAllAsync();
                    var existingStudentByName = allUsers.OfType<Student>().FirstOrDefault(s =>
                        s.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                        s.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase) &&
                        (string.IsNullOrWhiteSpace(middleName) && string.IsNullOrWhiteSpace(s.MiddleName) ||
                         !string.IsNullOrWhiteSpace(middleName) && s.MiddleName != null && s.MiddleName.Equals(middleName, StringComparison.OrdinalIgnoreCase)));

                    if (existingStudentByName != null)
                    {
                        response.FailureCount++;
                        response.Errors.Add($"Row {rowNumber}: Student with name '{firstName} {middleName} {lastName}' already exists in the database");
                        continue;
                    }

                    // Generate username
                    var username = GenerateUsername(firstName, lastName, middleName);

                    // Ensure username is unique
                    var existingUser = await _userRepository.GetByUsernameAsync(username);
                    if (existingUser != null)
                    {
                        int counter = 1;
                        string newUsername;
                        do
                        {
                            newUsername = $"{username}{counter}";
                            existingUser = await _userRepository.GetByUsernameAsync(newUsername);
                            counter++;
                        } while (existingUser != null);
                        username = newUsername;
                    }

                    // Generate random password
                    var generatedPassword = GenerateRandomPassword();

                    // Generate temporary student ID (format: TEMP-YYYY-XXXXX)
                    var temporaryStudentId = await GenerateTemporaryStudentId();

                    // Create new student with minimal required fields
                    var newStudent = new Student
                    {
                        Id = Guid.NewGuid(),
                        FirstName = firstName,
                        LastName = lastName,
                        MiddleName = middleName,
                        Username = username,
                        Email = $"{username}@temporary.com", // Temporary email, to be updated by student
                        PasswordHash = _passwordHashingService.HashPassword(generatedPassword),
                        UserRole = UserRole.Student,
                        Status = "Offline",
                        PhoneNumber = "0000000000", // Temporary phone, to be updated by student
                        StudentIdNumber = temporaryStudentId, // Temporary ID, to be replaced with real ID
                        GeneratedPassword = generatedPassword // Store the generated password
                    };

                    await _userRepository.AddAsync(newStudent);
                    await _userRepository.SaveChangesAsync();

                    response.SuccessCount++;
                    response.RegisteredStudents.Add(new StudentRegistrationResult
                    {
                        FullName = $"{firstName} {middleName} {lastName}".Replace("  ", " ").Trim(),
                        Username = username,
                        GeneratedPassword = generatedPassword,
                        TemporaryStudentId = temporaryStudentId
                    });
                }
                catch (Exception ex)
                {
                    response.FailureCount++;
                    response.Errors.Add($"Row {rowNumber}: {ex.Message}");
                }
            }

            return response;
        }

        private string GenerateUsername(string firstName, string lastName, string? middleName)
        {
            var username = string.IsNullOrWhiteSpace(middleName)
                ? $"{firstName.ToLower()}.{lastName.ToLower()}"
                : $"{firstName.ToLower()}.{middleName.ToLower()}.{lastName.ToLower()}";

            // Remove any spaces and special characters
            return new string(username.Where(c => char.IsLetterOrDigit(c) || c == '.').ToArray());
        }

        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<string> GenerateTemporaryStudentId()
        {
            var year = DateTime.UtcNow.Year;
            var allStudents = await _userRepository.GetAllAsync();
            var existingTempIds = allStudents.OfType<Student>()
                .Where(s => s.StudentIdNumber != null && s.StudentIdNumber.StartsWith($"TEMP-{year}-"))
                .Select(s => s.StudentIdNumber)
                .ToList();

            int counter = 1;
            string tempId;
            do
            {
                tempId = $"TEMP-{year}-{counter:D5}"; // Format: TEMP-2025-00001
                counter++;
            } while (existingTempIds.Contains(tempId));

            return tempId;
        }

        /// <summary>
        /// Validates if a student has completed their profile with all required fields
        /// </summary>
        public async Task<(bool IsComplete, string ErrorMessage)> ValidateStudentProfileComplete(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            
            if (user == null)
            {
                return (false, "User not found.");
            }

            if (user is not Student student)
            {
                // Non-students (Teachers, Staff, Admin) are considered complete by default
                return (true, string.Empty);
            }

            var missingFields = new List<string>();

            // Check email (not temporary)
            if (string.IsNullOrWhiteSpace(student.Email) || student.Email.EndsWith("@temporary.com"))
            {
                missingFields.Add("Email");
            }

            // Check phone number (not temporary)
            if (string.IsNullOrWhiteSpace(student.PhoneNumber) || student.PhoneNumber == "0000000000")
            {
                missingFields.Add("Phone Number");
            }

            // Check student ID
            if (string.IsNullOrWhiteSpace(student.StudentIdNumber))
            {
                missingFields.Add("Student ID Number");
            }

            // Check course
            if (string.IsNullOrWhiteSpace(student.Course))
            {
                missingFields.Add("Course");
            }

            // Check section
            if (string.IsNullOrWhiteSpace(student.Section))
            {
                missingFields.Add("Section");
            }

            // Check year
            if (string.IsNullOrWhiteSpace(student.Year))
            {
                missingFields.Add("Year");
            }

            // Check address fields
            if (string.IsNullOrWhiteSpace(student.Street))
            {
                missingFields.Add("Street");
            }

            if (string.IsNullOrWhiteSpace(student.CityMunicipality))
            {
                missingFields.Add("City/Municipality");
            }

            if (string.IsNullOrWhiteSpace(student.Province))
            {
                missingFields.Add("Province");
            }

            if (string.IsNullOrWhiteSpace(student.PostalCode))
            {
                missingFields.Add("Postal Code");
            }

            // Check ID pictures
            if (student.FrontStudentIdPicture == null || student.FrontStudentIdPicture.Length == 0)
            {
                missingFields.Add("Front Student ID Picture");
            }

            if (student.BackStudentIdPicture == null || student.BackStudentIdPicture.Length == 0)
            {
                missingFields.Add("Back Student ID Picture");
            }

            if (missingFields.Any())
            {
                var errorMessage = $"Profile incomplete. Please complete the following fields: {string.Join(", ", missingFields)}";
                return (false, errorMessage);
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// Retrieves a student by their student ID number
        /// </summary>
        public async Task<object?> GetStudentByIdNumberAsync(string studentIdNumber)
        {
            if (string.IsNullOrWhiteSpace(studentIdNumber))
            {
                return null;
            }

            var users = await _userRepository.GetAllAsync();
            var student = users.OfType<Student>()
                .FirstOrDefault(s => s.StudentIdNumber == studentIdNumber);

            if (student == null)
            {
                return null;
            }

            // Convert front and back ID pictures to base64 strings if they exist
            string? frontIdPictureBase64 = null;
            string? backIdPictureBase64 = null;

            if (student.FrontStudentIdPicture != null && student.FrontStudentIdPicture.Length > 0)
            {
                frontIdPictureBase64 = $"data:image/jpeg;base64,{Convert.ToBase64String(student.FrontStudentIdPicture)}";
            }

            if (student.BackStudentIdPicture != null && student.BackStudentIdPicture.Length > 0)
            {
                backIdPictureBase64 = $"data:image/jpeg;base64,{Convert.ToBase64String(student.BackStudentIdPicture)}";
            }

            return new
            {
                Id = student.Id,
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                LastName = student.LastName,
                StudentIdNumber = student.StudentIdNumber,
                Course = student.Course,
                Section = student.Section,
                Year = student.Year,
                Street = student.Street,
                Province = student.Province,
                PostalCode = student.PostalCode,
                CityMunicipality = student.CityMunicipality,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                FrontIdPicture = frontIdPictureBase64,
                BackIdPicture = backIdPictureBase64,
                GeneratedPassword = student.GeneratedPassword
            };
        }
    }
}
