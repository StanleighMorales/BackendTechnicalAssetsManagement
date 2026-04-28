using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.ActivityLog;
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
        private readonly ISupabaseStorageService _storageService;
        private readonly IItemRepository _itemRepository;
        private readonly ILentItemsRepository _lentItemsRepository;
        private readonly IActivityLogRepository _activityLogRepository;

        public UserService(IUserRepository userRepository, IMapper mapper, IArchiveUserService archiveUserService, IPasswordHashingService passwordHashingService, IExcelReaderService excelReaderService, ISupabaseStorageService storageService, IItemRepository itemRepository, ILentItemsRepository lentItemsRepository, IActivityLogRepository activityLogRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _archiveUserService = archiveUserService;
            _passwordHashingService = passwordHashingService;
            _excelReaderService = excelReaderService;
            _storageService = storageService;
            _itemRepository = itemRepository;
            _lentItemsRepository = lentItemsRepository;
            _activityLogRepository = activityLogRepository;
        }

        public async Task<BaseProfileDto?> GetUserProfileByIdAsync(Guid userId)
        {
            // 1. Fetch the user object with LentItems history (it will be the derived type: Student, Teacher, etc.)
            var user = await _userRepository.GetByIdWithHistoryAsync(userId);
            if (user == null)
            {
                return null; // Not found
            }

            // 2. Fetch the 5 most recent activities for this user
            var recentActivities = await _activityLogRepository.GetFilteredAsync(
                category: null,
                from: null,
                to: null,
                actorUserId: userId,
                itemId: null,
                status: null
            );
            
            var recentActivitiesList = recentActivities
                .OrderByDescending(a => a.CreatedAt)
                .Take(5)
                .ToList();

            // 3. Map activities to DTOs
            var recentActivitiesDto = _mapper.Map<List<ActivityLogDto>>(recentActivitiesList);

            // 4. Explicitly check the runtime type and map to the specific DTO.
            // This circumvents the occasional failure of AutoMapper's runtime polymorphism.
            BaseProfileDto profile;
            if (user is Student student)
            {
                profile = _mapper.Map<GetStudentProfileDto>(student);
                
                // 5. Populate ItemSummary array for students only
                var summary = await GetUserItemSummaryAsync(userId);
                ((GetStudentProfileDto)profile).ItemSummary = new List<ItemStatusCountDto>
                {
                    new ItemStatusCountDto { Status = "Reserved", Count = summary.ReservedCount },
                    new ItemStatusCountDto { Status = "Borrowed", Count = summary.BorrowedCount },
                    new ItemStatusCountDto { Status = "Returned", Count = summary.ReturnedCount },
                    new ItemStatusCountDto { Status = "Available", Count = summary.AvailableCount }
                };
            }
            else if (user is Teacher teacher)
            {
                profile = _mapper.Map<GetTeacherProfileDto>(teacher);
            }
            else if (user is Staff staff)
            {
                profile = _mapper.Map<GetStaffProfileDto>(staff);
            }
            else
            {
                // Fallback: If the user is a base User or an unknown type, map to the base profile DTO.
                profile = _mapper.Map<BaseProfileDto>(user);
            }

            // 6. Populate recent activities for all user types
            profile.RecentActivities = recentActivitiesDto;

            return profile;
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

            Console.WriteLine($"[DEBUG] Before update - StudentIdNumber: '{studentToUpdate.StudentIdNumber}', Course: '{studentToUpdate.Course}', Section: '{studentToUpdate.Section}'");
            Console.WriteLine($"[DEBUG] DTO values - StudentIdNumber: '{dto.StudentIdNumber}', Course: '{dto.Course}', Section: '{dto.Section}', PhoneNumber: '{dto.PhoneNumber}'");

            // Centralized validation
            try
            {
                if (dto.ProfilePicture != null) ImageConverterUtils.ValidateImage(dto.ProfilePicture);
                if (dto.FrontStudentIdPicture != null) ImageConverterUtils.ValidateImage(dto.FrontStudentIdPicture);
                if (dto.BackStudentIdPicture != null) ImageConverterUtils.ValidateImage(dto.BackStudentIdPicture);
            }
            catch (ArgumentException)
            {
                throw;
            }

            // Upload images to Supabase Storage and store URLs
            if (dto.ProfilePicture != null)
            {
                if (!string.IsNullOrEmpty(studentToUpdate.ProfilePictureUrl))
                    await _storageService.DeleteImageAsync(studentToUpdate.ProfilePictureUrl);
                studentToUpdate.ProfilePictureUrl = await _storageService.UploadImageAsync(dto.ProfilePicture, "students/profile");
            }
            if (dto.FrontStudentIdPicture != null)
            {
                if (!string.IsNullOrEmpty(studentToUpdate.FrontStudentIdPictureUrl))
                    await _storageService.DeleteImageAsync(studentToUpdate.FrontStudentIdPictureUrl);
                studentToUpdate.FrontStudentIdPictureUrl = await _storageService.UploadImageAsync(dto.FrontStudentIdPicture, "students/id-front");
            }
            if (dto.BackStudentIdPicture != null)
            {
                if (!string.IsNullOrEmpty(studentToUpdate.BackStudentIdPictureUrl))
                    await _storageService.DeleteImageAsync(studentToUpdate.BackStudentIdPictureUrl);
                studentToUpdate.BackStudentIdPictureUrl = await _storageService.UploadImageAsync(dto.BackStudentIdPicture, "students/id-back");
            }

            Console.WriteLine($"[DEBUG] Before AutoMapper.Map()");
            _mapper.Map(dto, studentToUpdate);
            Console.WriteLine($"[DEBUG] After AutoMapper.Map()");

            Console.WriteLine($"[DEBUG] After mapping - StudentIdNumber: '{studentToUpdate.StudentIdNumber}', Course: '{studentToUpdate.Course}', Section: '{studentToUpdate.Section}', PhoneNumber: '{studentToUpdate.PhoneNumber}'");

            await _userRepository.UpdateAsync(studentToUpdate);
            
            var saveResult = await _userRepository.SaveChangesAsync();
            Console.WriteLine($"[DEBUG] SaveChanges result: {saveResult}");
            Console.WriteLine($"[DEBUG] After save - StudentIdNumber: '{studentToUpdate.StudentIdNumber}', Course: '{studentToUpdate.Course}'");
            
            return saveResult;
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

            // TODO: Re-enable picture validation before production
            // Check ID pictures
            // if (student.FrontStudentIdPicture == null || student.FrontStudentIdPicture.Length == 0)
            // {
            //     missingFields.Add("Front Student ID Picture");
            // }

            // if (student.BackStudentIdPicture == null || student.BackStudentIdPicture.Length == 0)
            // {
            //     missingFields.Add("Back Student ID Picture");
            // }

            // TODO: Re-enable profile validation before production
            // if (missingFields.Any())
            // {
            //     var errorMessage = $"Profile incomplete. Please complete the following fields: {string.Join(", ", missingFields)}";
            //     return (false, errorMessage);
            // }

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
                FrontIdPicture = student.FrontStudentIdPictureUrl,
                BackIdPicture = student.BackStudentIdPictureUrl,
                GeneratedPassword = student.GeneratedPassword
            };        }

        public async Task<(bool Success, string ErrorMessage)> RegisterRfidToStudentAsync(Guid studentId, string rfidUid)
        {
            var (success, errorMessage) = await _userRepository.RegisterRfidToStudentAsync(studentId, rfidUid);
            if (!success) return (false, errorMessage);

            var saved = await _userRepository.SaveChangesAsync();
            return saved ? (true, string.Empty) : (false, "Failed to save RFID registration.");
        }

        public async Task<object?> GetStudentByRfidUidAsync(string rfidUid)
        {
            var student = await _userRepository.GetStudentByRfidUidAsync(rfidUid);
            if (student == null) return null;
            return new { student.Id, student.FirstName, student.LastName, student.StudentIdNumber };
        }

        public async Task<UserItemSummaryDto> GetUserItemSummaryAsync(Guid userId)
        {
            // Get all lent items for this specific user
            var allLentItems = await _lentItemsRepository.GetAllAsync();
            var userLentItems = allLentItems.Where(li => li.UserId == userId).ToList();

            // Count items by status from the user's borrow history
            var summary = new UserItemSummaryDto
            {
                // Reserved: User's lent items with Pending or Approved status (waiting to borrow)
                ReservedCount = userLentItems.Count(li => 
                    li.Status != null && 
                    (li.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase) || 
                     li.Status.Equals("Approved", StringComparison.OrdinalIgnoreCase))),
                
                // Borrowed: User's lent items with Borrowed status (currently borrowed)
                BorrowedCount = userLentItems.Count(li => 
                    li.Status != null && 
                    li.Status.Equals("Borrowed", StringComparison.OrdinalIgnoreCase)),
                
                // Returned: User's lent items with Returned status
                ReturnedCount = userLentItems.Count(li => 
                    li.Status != null && 
                    li.Status.Equals("Returned", StringComparison.OrdinalIgnoreCase)),
                
                // Available: Items that are available in the system (not user-specific, for reference)
                AvailableCount = (await _itemRepository.GetAllAsync())
                    .Count(i => i.Status == Enums.ItemStatus.Available)
            };

            return summary;
        }

        public async Task<(bool Success, string ErrorMessage)> BlockUserAsync(Guid userId, BlockUserDto dto, Guid blockedById)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return (false, "User not found.");
            }

            // Get the blocker's information to validate role hierarchy
            var blocker = await _userRepository.GetByIdAsync(blockedById);
            if (blocker == null)
            {
                return (false, "Blocker user not found.");
            }

            // Prevent blocking SuperAdmin
            if (user.UserRole == Enums.UserRole.SuperAdmin)
            {
                return (false, "Cannot block a SuperAdmin account.");
            }

            // Role hierarchy validation
            // Staff can only block Teachers and Students
            if (blocker.UserRole == Enums.UserRole.Staff)
            {
                if (user.UserRole == Enums.UserRole.Admin || user.UserRole == Enums.UserRole.Staff)
                {
                    return (false, "Staff members can only block Teachers and Students.");
                }
            }

            // Admin cannot block other Admins or SuperAdmins (SuperAdmin already checked above)
            if (blocker.UserRole == Enums.UserRole.Admin)
            {
                if (user.UserRole == Enums.UserRole.Admin)
                {
                    return (false, "Admins cannot block other Admin accounts.");
                }
            }

            // Prevent self-blocking
            if (userId == blockedById)
            {
                return (false, "You cannot block your own account.");
            }

            // Validate temporary ban has an end date
            if (!dto.IsPermanent && dto.BlockedUntil == null)
            {
                return (false, "Temporary ban requires an end date.");
            }

            // Validate end date is in the future
            if (!dto.IsPermanent && dto.BlockedUntil <= DateTime.UtcNow)
            {
                return (false, "Block end date must be in the future.");
            }

            user.IsBlocked = true;
            user.BlockReason = dto.Reason;
            user.BlockedAt = DateTime.UtcNow;
            user.BlockedUntil = dto.IsPermanent ? null : dto.BlockedUntil;
            user.BlockedById = blockedById;

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return (true, "User blocked successfully.");
        }

        public async Task<(bool Success, string ErrorMessage)> UnblockUserAsync(Guid userId, Guid unblockedById)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return (false, "User not found.");
            }

            if (!user.IsBlocked)
            {
                return (false, "User is not currently blocked.");
            }

            // Get the unblocker's information to validate role hierarchy
            var unblocker = await _userRepository.GetByIdAsync(unblockedById);
            if (unblocker == null)
            {
                return (false, "Unblocker user not found.");
            }

            // Role hierarchy validation - same rules as blocking
            // Staff can only unblock Teachers and Students
            if (unblocker.UserRole == Enums.UserRole.Staff)
            {
                if (user.UserRole == Enums.UserRole.Admin || user.UserRole == Enums.UserRole.Staff || user.UserRole == Enums.UserRole.SuperAdmin)
                {
                    return (false, "Staff members can only unblock Teachers and Students.");
                }
            }

            // Admin cannot unblock other Admins or SuperAdmins
            if (unblocker.UserRole == Enums.UserRole.Admin)
            {
                if (user.UserRole == Enums.UserRole.Admin || user.UserRole == Enums.UserRole.SuperAdmin)
                {
                    return (false, "Admins cannot unblock Admin or SuperAdmin accounts.");
                }
            }

            user.IsBlocked = false;
            user.BlockReason = null;
            user.BlockedAt = null;
            user.BlockedUntil = null;
            user.BlockedById = null;

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return (true, "User unblocked successfully.");
        }

        public async Task<BlockStatusDto?> GetBlockStatusAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            string? blockedByUsername = null;
            if (user.BlockedById.HasValue)
            {
                var blockedByUser = await _userRepository.GetByIdAsync(user.BlockedById.Value);
                blockedByUsername = blockedByUser?.Username;
            }

            return new BlockStatusDto
            {
                IsBlocked = user.IsBlocked,
                BlockReason = user.BlockReason,
                BlockedAt = user.BlockedAt,
                BlockedUntil = user.BlockedUntil,
                IsPermanent = user.IsBlocked && user.BlockedUntil == null,
                BlockedById = user.BlockedById,
                BlockedByUsername = blockedByUsername
            };
        }
    }
}
