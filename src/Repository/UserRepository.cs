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
            return await _context.SaveChangesAsync() > 0;
        }

        public Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            return Task.CompletedTask;
        }

        public async Task<Student?> GetStudentByRfidUidAsync(string rfidUid)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.RfidUid == rfidUid);
        }

        public async Task<(bool Success, string ErrorMessage)> RegisterRfidToStudentAsync(Guid studentId, string rfidCode)
        {
            // Step 1: Look up the RFID in the RFID table by code
            var rfidCard = await _context.Rfids
                .FirstOrDefaultAsync(r => r.RfidCode == rfidCode);
            
            if (rfidCard == null)
                return (false, $"RFID code '{rfidCode}' not found in the system. Please contact admin.");

            // Step 2: Get the student who is trying to register
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == studentId);
            
            if (student == null)
                return (false, "Student not found.");

            // Step 3: Check if this student already has an RFID registered
            if (!string.IsNullOrEmpty(student.RfidUid) || !string.IsNullOrEmpty(student.RfidCode))
            {
                // Check if they're trying to register the same RFID again
                if (student.RfidCode == rfidCode)
                    return (false, $"You have already registered this RFID card.");
                
                // They're trying to register a different RFID
                return (false, $"You already have an RFID card registered (Code: {student.RfidCode}). Please contact admin to change it.");
            }

            // Step 4: Check if this RFID is already assigned to another student
            var existingStudent = await _context.Students
                .FirstOrDefaultAsync(s => (s.RfidUid == rfidCard.RfidUid || s.RfidCode == rfidCode) 
                                          && s.Id != studentId);
            
            if (existingStudent != null)
                return (false, $"RFID code '{rfidCode}' is already registered to another student.");

            // Step 5: All validations passed - register the RFID
            student.RfidUid = rfidCard.RfidUid;      // The actual UID from physical card
            student.RfidCode = rfidCard.RfidCode;    // The human-readable code
            _context.Students.Update(student);
            return (true, string.Empty);
        }
    }
}
