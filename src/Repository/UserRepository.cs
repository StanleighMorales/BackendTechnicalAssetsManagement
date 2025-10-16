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
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<StaffDto>> GetAllStaffAsync()
        {
            // Use the Staff -> StaffDto mapping you defined
            return await _context.Users.OfType<Staff>()
                .ProjectTo<StaffDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            // Use the Student -> StudentDto mapping you defined (which includes base64 conversion)
            return await _context.Users.OfType<Student>()
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherDto>> GetAllTeachersAsync()
        {
            // Use the Teacher -> TeacherDto mapping you defined
            return await _context.Users.OfType<Teacher>()
                .ProjectTo<TeacherDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllUserDtosAsync()
        {
            var allUsers = new List<UserDto>();

            // --- Execute each query sequentially to avoid DbContext concurrency issues ---

            // 1. Get all students
            var students = await _context.Users.OfType<Student>()
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    Username = s.Username,
                    Email = s.Email,
                    UserRole = s.UserRole,
                    Status = s.Status,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    MiddleName = s.MiddleName,
                    StudentIdNumber = s.StudentIdNumber,
                    PhoneNumber = s.PhoneNumber,
                    Course = s.Course,
                    Section = s.Section,
                    Year = s.Year,
                    Street = s.Street,
                    CityMunicipality = s.CityMunicipality,
                    Province = s.Province,
                    PostalCode = s.PostalCode
                }).ToListAsync();
            allUsers.AddRange(students);

            // 2. Get all teachers
            var teachers = await _context.Users.OfType<Teacher>()
                .Select(t => new TeacherDto
                {
                    Id = t.Id,
                    Username = t.Username,
                    Email = t.Email,
                    UserRole = t.UserRole,
                    Status = t.Status,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    MiddleName = t.MiddleName,
                    Department = t.Department
                }).ToListAsync();
            allUsers.AddRange(teachers);

            // 3. Get all staff
            var staff = await _context.Users.OfType<Staff>()
                .Select(s => new StaffDto
                {
                    Id = s.Id,
                    Username = s.Username,
                    Email = s.Email,
                    UserRole = s.UserRole,
                    Status = s.Status,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    MiddleName = s.MiddleName,
                    Position = s.Position,
                    PhoneNumber = s.PhoneNumber
                }).ToListAsync();
            allUsers.AddRange(staff);

            

            return allUsers;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email);
        }

        //public async Task<User?> GetByIdAsync(Guid id)
        //{
        //    return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        //}
        public async Task<User?> GetByIdAsync(Guid id)
        {
            // NEW: Eagerly load the LentItems collection and its required navigation properties
            return await _context.Users
                .Include(u => u.LentItems
                .Where(li => !li.IsHiddenFromUser).OrderByDescending(li => li.LentAt))
                    
                    // If you need Item or Teacher details for the LentItemsDto mapping
                    // you must include them here. 
                    .ThenInclude(li => li.Item) // Include the Item details for the LentItemsDto mapping
                .Include(u => u.LentItems
                    .Where(li => !li.IsHiddenFromUser) // <--- CRITICAL FIX: Add the filter to this chain
                    .OrderByDescending(li => li.LentAt))
                    .ThenInclude(li => li.Teacher) // Include the Teacher details for the LentItemsDto mapping
                .FirstOrDefaultAsync(u => u.Id == id);
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

            return await _context.Users.OfType<Staff>().FirstOrDefaultAsync(s => s.PhoneNumber == phoneNumber);
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
    }
}
