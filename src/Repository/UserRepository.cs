using AutoMapper;
using AutoMapper.QueryableExtensions;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.Data;
using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalAssetsManagement.src.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return user;
        }

        public async Task DeleteAsync(Guid id)
        {
            var userToDelete = await _context.Users.FindAsync(id);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
            }

        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<StaffDto>> GetAllStaffAsync()
        {
            return await _context.Users.OfType<Staff>()
                .AsNoTracking()
                .ProjectTo<StaffDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            return await _context.Users.OfType<Student>()
                .AsNoTracking()
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherDto>> GetAllTeachersAsync()
        {
            return await _context.Users.OfType<Teacher>()
                .AsNoTracking()
                .ProjectTo<TeacherDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllUserDtosAsync()
        {
            var allUsers = await _context.Users.AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(allUsers);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email);
        }

        //public async Task<User?> GetByIdAsync(Guid id)
        //{
        //    return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        //}
        public async Task<User?> GetByIdAsync(Guid id)
        {
            // Load the user without LentItems — callers that need LentItems
            // should use a dedicated method to avoid loading the full history on every lookup.
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        /// <summary>
        /// Loads a user together with their full LentItems history (including Item and Teacher).
        /// Use this only when the history is actually needed — not for auth or profile lookups.
        /// </summary>
        public async Task<User?> GetByIdWithHistoryAsync(Guid id)
        {
            var user = await _context.Users
                .Include(u => u.LentItems)
                    .ThenInclude(li => li.Item)
                .Include(u => u.LentItems)
                    .ThenInclude(li => li.Teacher)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user?.LentItems != null)
            {
                user.LentItems = user.LentItems
                    .Where(li => !li.IsHiddenFromUser)
                    .OrderByDescending(li => li.CreatedAt)
                    .ToList();
            }

            return user;
        }

        public async Task<User?> GetByIdentifierAsync(string identifier)
        {
            var normalizedIdentifier = identifier.Trim().ToLower();

            return await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == normalizedIdentifier || (u.Email != null && u.Email.ToLower() == normalizedIdentifier));
        }

        public async Task<User?> GetByPhoneNumberAsync(string? phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return null;
            }

            return await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<bool> SaveChangesAsync()
        {
            var changeCount = await _context.SaveChangesAsync();
            Console.WriteLine($"[DEBUG] SaveChangesAsync - Changes saved: {changeCount}");
            return changeCount > 0;
        }

        public Task UpdateAsync(User user)
        {
            var entry = _context.Entry(user);
            Console.WriteLine($"[DEBUG] UpdateAsync - Entity State before Update(): {entry.State}");
            
            _context.Users.Update(user);
            
            entry = _context.Entry(user);
            Console.WriteLine($"[DEBUG] UpdateAsync - Entity State after Update(): {entry.State}");
            
            // Log modified properties
            var modifiedProperties = entry.Properties
                .Where(p => p.IsModified)
                .Select(p => $"{p.Metadata.Name}={p.CurrentValue}")
                .ToList();
            Console.WriteLine($"[DEBUG] UpdateAsync - Modified properties: {string.Join(", ", modifiedProperties)}");
            
            return Task.CompletedTask;
        }

        public async Task<Student?> GetStudentByRfidUidAsync(string rfidUid)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.RfidUid == rfidUid);
        }

        public async Task<(bool Success, string ErrorMessage)> RegisterRfidToStudentAsync(Guid studentId, string rfidUid)
        {
            // Step 1: Get the student
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == studentId);
            
            if (student == null)
                return (false, "Student not found.");

            // Step 2: Check if this student already has an RFID registered
            if (!string.IsNullOrEmpty(student.RfidUid))
            {
                if (student.RfidUid == rfidUid)
                    return (false, "You have already registered this RFID card.");
                
                return (false, "You already have an RFID card registered. Please contact admin to change it.");
            }

            // Step 3: Check if this RFID UID is already assigned to another student
            var existingStudent = await _context.Students
                .FirstOrDefaultAsync(s => s.RfidUid == rfidUid && s.Id != studentId);
            
            if (existingStudent != null)
                return (false, $"RFID '{rfidUid}' is already registered to another student.");

            // Step 4: All validations passed - assign the UID directly
            student.RfidUid = rfidUid;
            _context.Students.Update(student);
            return (true, string.Empty);
        }
    }
}
