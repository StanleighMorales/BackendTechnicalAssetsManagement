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
                .Include<Admin, AdminDto>()
                .IncludeAllDerived();

            CreateMap<Staff, StaffDto>();
            CreateMap<Teacher, TeacherDto>();
            CreateMap<Admin, AdminDto>();

            /// <summary>
            /// Specific mapping for Student to handle converting image byte[] fields to base64 strings for the client.
            /// </summary>
            CreateMap<Student, StudentDto>()
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
             .Include<Admin, GetAdminProfileDto>()
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
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course))
                .ForMember(dest => dest.Section, opt => opt.MapFrom(src => src.Section))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.CityMunicipality, opt => opt.MapFrom(src => src.CityMunicipality))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode));

            CreateMap<Teacher, GetTeacherProfileDto>()
                .IncludeBase<User, BaseProfileDto>()
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department));

            CreateMap<Staff, GetStaffProfileDto>()
                .IncludeBase<User, BaseProfileDto>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position));

            CreateMap<Admin, GetAdminProfileDto>()
                .IncludeBase<User, BaseProfileDto>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            #endregion


            #region DTO to Model Mappings (For API Requests)

            /// <summary>
            /// Configures base mapping for all derived Register DTOs to their User models (for registration).
            /// </summary>
            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => src.Role))
                .Include<RegisterStudentDto, Student>() // Use specific DTOs
                .Include<RegisterTeacherDto, Teacher>()
                .Include<RegisterStaffDto, Staff>()
                .Include<RegisterAdminDto, Admin>();

            // Explicit maps for derived types from the base RegisterUserDto
            CreateMap<RegisterStudentDto, Student>();
            CreateMap<RegisterTeacherDto, Teacher>();
            CreateMap<RegisterStaffDto, Staff>();
            CreateMap<RegisterAdminDto, Admin>();

            /// <summary>
            /// Consolidated mapping for Student profile updates. 
            /// 1. Converts IFormFile to byte[] ONLY if the file is provided.
            /// 2. Ignores all other null properties for partial updates.
            /// </summary>
            CreateMap<UpdateStudentProfileDto, Student>()
               // Rule 1: Handle the image properties with specific logic
               .ForMember(dest => dest.ProfilePicture, opt => {
                   opt.Condition(src => src.ProfilePicture != null);
                   opt.MapFrom(src => ImageConverterUtils.ConvertIFormFileToByteArray(src.ProfilePicture));
               })
               .ForMember(dest => dest.FrontStudentIdPicture, opt => {
                   opt.Condition(src => src.FrontStudentIdPicture != null);
                   opt.MapFrom(src => ImageConverterUtils.ConvertIFormFileToByteArray(src.FrontStudentIdPicture));
               })
               .ForMember(dest => dest.BackStudentIdPicture, opt => {
                   opt.Condition(src => src.BackStudentIdPicture != null);
                   opt.MapFrom(src => ImageConverterUtils.ConvertIFormFileToByteArray(src.BackStudentIdPicture));
               })
               // Rule 2: Handle all other properties (e.g., LastName, Course) to ignore nulls for partial updates.
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

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

            /// <summary>
            /// Mapping for Admin profile updates, ignoring null properties for partial updates.
            /// </summary>
            CreateMap<UpdateAdminProfileDto, Admin>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            // --- Generic DTO to Model (used for generic mapping like in repository or service layer) ---
            CreateMap<UserDto, User>()
                .Include<TeacherDto, Teacher>()
                .Include<StudentDto, Student>()
                .Include<StaffDto, Staff>()
                .Include<AdminDto, Admin>();

            CreateMap<StaffDto, Staff>();
            CreateMap<TeacherDto, Teacher>();
            CreateMap<StudentDto, Student>();
            CreateMap<AdminDto, Admin>();

            #endregion
        }
    }
}