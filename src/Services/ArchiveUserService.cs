using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.DTOs.Archive.Users;
using BackendTechnicalAssetsManagement.src.IRepository;
using BackendTechnicalAssetsManagement.src.IService;
using static BackendTechnicalAssetsManagement.src.Classes.Enums.UserRole;

namespace BackendTechnicalAssetsManagement.src.Services
{
    public class ArchiveUserService : IArchiveUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IArchiveUserRepository _archiveUserRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context; // For transaction management

        public ArchiveUserService(
            IUserRepository userRepository,
            IArchiveUserRepository archiveUserRepository,
            IMapper mapper,
            AppDbContext context)
        {
            _userRepository = userRepository;
            _archiveUserRepository = archiveUserRepository;
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Retrieves all archived users and maps them to their DTOs.
        /// </summary>
        public async Task<IEnumerable<ArchiveUserDto>> GetAllArchivedUsersAsync()
        {
            // This now calls the repository method that returns the fully-formed DTOs.
            return await _archiveUserRepository.GetAllArchiveUserDtosAsync();
        }

        /// <summary>
        /// Retrieves a single archived user by their ID.
        /// </summary>
        public async Task<ArchiveUserDto?> GetArchivedUserByIdAsync(Guid archiveUserId)
        {
            var archivedUser = await _archiveUserRepository.GetByIdAsync(archiveUserId);
            // Map the entity to its corresponding DTO
            return _mapper.Map<ArchiveUserDto?>(archivedUser);
        }

        /// <summary>
        /// Moves an active user to the archive table within a database transaction.
        /// SuperAdmins cannot be archived.
        /// </summary>
        public async Task<(bool Success, string ErrorMessage)> ArchiveUserAsync(Guid targetUserId, Guid currentUserId)
        {
            // NEW VALIDATION: Prevent self-archiving
            if (targetUserId == currentUserId)
            {
                // You cannot archive yourself.
                return (false, "Cannot archive your own account.");
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var userToArchive = await _userRepository.GetByIdAsync(targetUserId);

                // Check if user exists
                if (userToArchive == null)
                {
                    await transaction.RollbackAsync();
                    return (false, "User not found.");
                }

                // Existing validation for SuperAdmin
                if (userToArchive.UserRole == SuperAdmin)
                {
                    await transaction.RollbackAsync();
                    return (false, "SuperAdmin users cannot be archived.");
                }

                // NEW VALIDATION: Check user status
                // Using OrdinalIgnoreCase is a robust way to compare strings like "Online", "online", "ONLINE", etc.
                if (userToArchive.Status?.Equals("Online", StringComparison.OrdinalIgnoreCase) == true)
                {
                    // You cannot archive a user who is currently online.
                    await transaction.RollbackAsync();
                    return (false, "Cannot archive a user who is currently online.");
                }
                userToArchive.Status = "Archived";

                // ... The rest of the method (mapping, adding, deleting, committing) remains the same ...
                var archivedUser = _mapper.Map<ArchiveUser>(userToArchive);
                await _archiveUserRepository.AddAsync(archivedUser);
                await _archiveUserRepository.SaveChangesAsync();
                await _userRepository.DeleteAsync(targetUserId); // Use targetUserId here
                await _userRepository.SaveChangesAsync();
                await transaction.CommitAsync();
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Archive operation failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Restores an archived user back to the active user table within a transaction.
        /// </summary>
        public async Task<bool> RestoreUserAsync(Guid archiveUserId)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Fetch the archived user
                var userToRestore = await _archiveUserRepository.GetByIdAsync(archiveUserId);
                if (userToRestore == null)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                // 2. Map the archived user back to an active user entity
                // The mapping profile will handle resetting the Status, etc.
                
                var restoredUser = _mapper.Map<Classes.User>(userToRestore);
                restoredUser.Status = "Offline"; // Explicitly set status to Active
                // 3. Add the user back to the active user table
                await _userRepository.AddAsync(restoredUser);
                await _userRepository.SaveChangesAsync();

                // 4. Delete the user from the archive table
                await _archiveUserRepository.DeleteAsync(archiveUserId);
                await _archiveUserRepository.SaveChangesAsync();

                // 5. If all operations succeed, commit the transaction
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                // 6. If any error occurs, roll back all changes
                await transaction.RollbackAsync();
                return false;
            }
        }

        /// <summary>
        /// Permanently deletes a user from the archive table.
        /// </summary>
        public async Task<bool> PermanentDeleteArchivedUserAsync(Guid archiveUserId)
        {
            // Check if the user exists before trying to delete
            var userExists = await _archiveUserRepository.GetByIdAsync(archiveUserId) != null;
            if (!userExists)
            {
                return false;
            }

            await _archiveUserRepository.DeleteAsync(archiveUserId);
            return await _archiveUserRepository.SaveChangesAsync();
        }
    }
}