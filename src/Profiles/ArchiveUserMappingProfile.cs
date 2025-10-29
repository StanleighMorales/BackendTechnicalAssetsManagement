using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.Archive.Users;

namespace BackendTechnicalAssetsManagement.src.Profiles
{
    public class ArchiveUserMappingProfile : Profile
    {
        public ArchiveUserMappingProfile()
        {
            #region Active User (Model) to Archive User (Model) - For Archiving
            // Base mapping from active User to ArchiveUser
            CreateMap<User, ArchiveUser>()
                .ForMember(dest => dest.OriginalUserId, opt => opt.MapFrom(src => src.Id)) // Link to original ID
                .ForMember(dest => dest.ArchivedAt, opt => opt.MapFrom(src => DateTime.UtcNow)) // Set archive timestamp
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Archived")) // Set status upon archiving
                                                                                       // Handle properties that should not be copied or have specific logic (e.g., collections)
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash)) // Copy password hash to archive
                                                                                                   // Exclude properties that don't exist in ArchiveUser or shouldn't be copied directly
                                                                                                   // .ForMember(dest => dest.RefreshTokens, opt => opt.Ignore()) // If RefreshTokens don't go to archive
                                                                                                   // .ForMember(dest => dest.LentItems, opt => opt.Ignore())     // If LentItems don't go to archive
                .Include<Student, ArchiveStudent>()
                .Include<Teacher, ArchiveTeacher>()
                .Include<Staff, ArchiveStaff>();

            // Derived mappings from active to archive
            CreateMap<Student, ArchiveStudent>()
                .IncludeBase<User, ArchiveUser>(); // Ensures base properties are mapped
            CreateMap<Teacher, ArchiveTeacher>()
                .IncludeBase<User, ArchiveUser>();
            CreateMap<Staff, ArchiveStaff>()
                .IncludeBase<User, ArchiveUser>();
            #endregion

            #region Archive User (Model) to Archive User DTO (For API Responses)
            // Base mapping from ArchiveUser to ArchiveUserDto
            CreateMap<ArchiveUser, ArchiveUserDto>()
                .Include<ArchiveStudent, ArchiveStudentDto>()
                .Include<ArchiveTeacher, ArchiveTeacherDto>()
                .Include<ArchiveStaff, ArchiveStaffDto>();

            // Derived mappings from Archive model to Archive DTO
            CreateMap<ArchiveStudent, ArchiveStudentDto>()
                .IncludeBase<ArchiveUser, ArchiveUserDto>()
                // Map byte[] to string (Base64) for images, similar to your active StudentDto mapping
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src =>
                    src.ProfilePicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.ProfilePicture)}" : null))
                .ForMember(dest => dest.FrontStudentIdPicture, opt => opt.MapFrom(src =>
                    src.FrontStudentIdPicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.FrontStudentIdPicture)}" : null))
                .ForMember(dest => dest.BackStudentIdPicture, opt => opt.MapFrom(src =>
                    src.BackStudentIdPicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.BackStudentIdPicture)}" : null));

            CreateMap<ArchiveTeacher, ArchiveTeacherDto>()
                .IncludeBase<ArchiveUser, ArchiveUserDto>();

            CreateMap<ArchiveStaff, ArchiveStaffDto>()
                .IncludeBase<ArchiveUser, ArchiveUserDto>();
            #endregion

            // For restoration purposes, if you need to map ArchiveUser back to User
            #region Archive User (Model) to Active User (Model) - For Restoration
            CreateMap<ArchiveUser, User>()
                // We typically use the ArchiveUser's ID when restoring, but we need to ensure it doesn't conflict
                // or you might want to generate a NEW ID. For now, let's assume we copy the ArchiveUser's ID.
                // You might need ValueGeneratedNever for User.Id in OnModelCreating if restoring with specific IDs.
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                // You might want to reset PasswordHash or re-prompt for it upon restoration
                // For now, copy it directly.
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Active")) // Set status upon restoring
                .Include<ArchiveStudent, Student>()
                .Include<ArchiveTeacher, Teacher>()
                .Include<ArchiveStaff, Staff>();

            CreateMap<ArchiveStudent, Student>()
                .IncludeBase<ArchiveUser, User>()
                // For images, when restoring, you'd map the byte[] directly, not Base64 strings.
                // So no specific ForMember for images needed here if they're byte[] in both.
                ;
            CreateMap<ArchiveTeacher, Teacher>()
                .IncludeBase<ArchiveUser, User>();
            CreateMap<ArchiveStaff, Staff>()
                .IncludeBase<ArchiveUser, User>();
            #endregion
        }
    }
}