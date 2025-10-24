// File: BackendTechnicalAssetsManagement.src.Profiles/UserMappingProfile.cs

using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;
using BackendTechnicalAssetsManagement.src.Utils;
using static BackendTechnicalAssetsManagement.src.DTOs.User.UserProfileDtos;

namespace BackendTechnicalAssetsManagement.src.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            #region Model to DTO Mappings (For API Responses)
            /// <summary>
            /// Configures base mapping for all derived User types to their DTOs (e.g., Student -> StudentDto).
            /// Handles base properties like Id, Username, Email, etc.
            /// </summary>
            CreateMap<User, UserDto>()
                .Include<Teacher, TeacherDto>()
                .Include<Student, StudentDto>()
                .Include<Staff, StaffDto>()
                .IncludeAllDerived();

            CreateMap<Staff, StaffDto>();
            CreateMap<Teacher, TeacherDto>();

            /// <summary>
            /// Specific mapping for Student to handle converting image byte[] fields to base64 strings for the client.
            /// </summary>
            CreateMap<Student, StudentDto>()
                .IncludeBase<User, UserDto>()
                // FIX: Add explicit mapping for StudentIdNumber and all other Student-specific fields 
                // to ensure they show up in the response.
                .ForMember(dest => dest.StudentIdNumber, opt => opt.MapFrom(src => src.StudentIdNumber))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course))
                .ForMember(dest => dest.Section, opt => opt.MapFrom(src => src.Section))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.CityMunicipality, opt => opt.MapFrom(src => src.CityMunicipality))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src =>
                    src.ProfilePicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.ProfilePicture)}" : null))
                .ForMember(dest => dest.FrontStudentIdPicture, opt => opt.MapFrom(src =>
                    src.FrontStudentIdPicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.FrontStudentIdPicture)}" : null))
                .ForMember(dest => dest.BackStudentIdPicture, opt => opt.MapFrom(src =>
                    src.BackStudentIdPicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.BackStudentIdPicture)}" : null));

            /// <summary>
            /// Configures base mapping for all derived User types to their specific Profile DTOs (for 'GetMyProfile').
            /// </summary>
            CreateMap<User, BaseProfileDto>()
             .Include<Teacher, GetTeacherProfileDto>()
             .Include<Staff, GetStaffProfileDto>()
             .Include<Student, GetStudentProfileDto>(); // Added Include for Student for completeness

            /// <summary>
            /// Specific profile mapping for Student, converting images to base64 and mapping all specific fields.
            /// </summary>
            CreateMap<Student, GetStudentProfileDto>()
                .IncludeBase<User, BaseProfileDto>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src =>
                    src.ProfilePicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.ProfilePicture)}" : null))
                .ForMember(dest => dest.FrontStudentIdPicture, opt => opt.MapFrom(src =>
                    src.FrontStudentIdPicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.FrontStudentIdPicture)}" : null))
                .ForMember(dest => dest.BackStudentIdPicture, opt => opt.MapFrom(src =>
                    src.BackStudentIdPicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.BackStudentIdPicture)}" : null))
                .ForMember(dest => dest.StudentIdNumber, opt => opt.MapFrom(src => src.StudentIdNumber))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course))
                .ForMember(dest => dest.Section, opt => opt.MapFrom(src => src.Section))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.CityMunicipality, opt => opt.MapFrom(src => src.CityMunicipality))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.LentItemsHistory, opt => opt.MapFrom(src => src.LentItems));

            CreateMap<Teacher, GetTeacherProfileDto>()
                .IncludeBase<User, BaseProfileDto>()
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.LentItemsHistory, opt => opt.MapFrom(src => src.LentItems));

            CreateMap<Staff, GetStaffProfileDto>()
                .IncludeBase<User, BaseProfileDto>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.LentItemsHistory, opt => opt.MapFrom(src => src.LentItems));


            #endregion


            #region DTO to Model Mappings (For API Requests)

            /// <summary>
            /// Configures base mapping for all derived Register DTOs to their User models (for registration).
            /// </summary>
            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => src.Role));

            // Explicit maps for derived types from the base RegisterUserDto
            CreateMap<RegisterStudentDto, Student>()
                // IncludeBase is now optional, but let's keep it to handle base User properties like Username, Email
                .IncludeBase<RegisterUserDto, User>()


                // And all the other derived properties (which also includes the DTO-to-Model logic)
                .ForMember(dest => dest.StudentIdNumber, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.StudentIdNumber) ? null : src.StudentIdNumber))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.Course) ? null : src.Course))
                // ... (All other derived Student properties) ...
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.Section, opt => opt.MapFrom(src => src.Section))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.CityMunicipality, opt => opt.MapFrom(src => src.CityMunicipality))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode));
            CreateMap<RegisterTeacherDto, Teacher>()
                .IncludeBase<RegisterUserDto, User>();
            CreateMap<RegisterStaffDto, Staff>()
                .IncludeBase<RegisterUserDto, User>();

            /// <summary>
            /// Hybrid mapping for Student profile updates.
            /// It explicitly handles the complex image properties while using a generic
            /// rule for all other simple properties. This is a clean and efficient pattern.
            /// </summary>
            CreateMap<UpdateStudentProfileDto, Student>()

                // --- Step 1: Explicitly handle the complex properties (the images) ---
                // Use the robust (src, dest) resolver to either update the image or keep the original.
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom((src, dest) =>
                    (src.ProfilePicture != null && src.ProfilePicture.Length > 0)
                        ? ImageConverterUtils.ConvertIFormFileToByteArray(src.ProfilePicture)
                        : dest.ProfilePicture))

                .ForMember(dest => dest.FrontStudentIdPicture, opt => opt.MapFrom((src, dest) =>
                    (src.FrontStudentIdPicture != null && src.FrontStudentIdPicture.Length > 0)
                        ? ImageConverterUtils.ConvertIFormFileToByteArray(src.FrontStudentIdPicture)
                        : dest.FrontStudentIdPicture))

                .ForMember(dest => dest.BackStudentIdPicture, opt => opt.MapFrom((src, dest) =>
                    (src.BackStudentIdPicture != null && src.BackStudentIdPicture.Length > 0)
                        ? ImageConverterUtils.ConvertIFormFileToByteArray(src.BackStudentIdPicture)
                        : dest.BackStudentIdPicture))

                // --- Step 2: Use a generic rule for all other members ---
                // This ForAllMembers rule will run for EVERY property, including the images.
                .ForAllMembers(opts => {
                    // We add a condition to tell this rule to IGNORE the properties we already handled.
                    if (opts.DestinationMember.Name == nameof(Student.ProfilePicture) ||
                        opts.DestinationMember.Name == nameof(Student.FrontStudentIdPicture) ||
                        opts.DestinationMember.Name == nameof(Student.BackStudentIdPicture))
                    {
                        // By calling UseDestinationValue(), we tell the mapper to stop processing
                        // and just keep the value that's already on the destination object.
                        // This prevents this generic rule from interfering with our specific rules above.
                        opts.UseDestinationValue();
                    }

                    // For all other properties (LastName, Course, etc.), apply the simple "ignore if null" rule.
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });


            /// <summary>
            /// Mapping for Teacher profile updates, ignoring null properties for partial updates.
            /// </summary>
            CreateMap<UpdateTeacherProfileDto, Teacher>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            /// <summary>
            /// Mapping for Staff profile updates, ignoring null properties for partial updates.
            /// </summary>
            CreateMap<UpdateStaffProfileDto, Staff>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateStaffProfileDto, User>() // Map the DTO to the base User class (Admin is a User)
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // --- Generic DTO to Model (used for generic mapping like in repository or service layer) ---
            CreateMap<UserDto, User>()
                .Include<TeacherDto, Teacher>()
                .Include<StudentDto, Student>()
                .Include<StaffDto, Staff>();

            CreateMap<StaffDto, Staff>();
            CreateMap<TeacherDto, Teacher>();
            CreateMap<StudentDto, Student>();

            #endregion
        }
    }
}