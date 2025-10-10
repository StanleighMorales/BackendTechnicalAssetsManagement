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

            // This region defines how to map your internal database models to the DTOs
            // that your API sends back to the client.

            // --- Base Output Mappings ---
            // Configures the base mapping for all user types to their corresponding DTOs.
            // AutoMapper will correctly use the more specific map (e.g., Student -> StudentDto) when needed.
            CreateMap<User, UserDto>()
                .Include<Teacher, TeacherDto>()
                .Include<Student, StudentDto>()
                .Include<Staff, StaffDto>()
                .Include<Admin, AdminDto>()
                .IncludeAllDerived();

            CreateMap<Staff, StaffDto>();
            CreateMap<Teacher, TeacherDto>();
            CreateMap<Admin, AdminDto>();

            // Specific mapping for Student to handle converting the image byte[] to a base64 string for the client.
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src =>
                    src.ProfilePicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.ProfilePicture)}" : null))
                .ForMember(dest => dest.FrontStudentIdPicture, opt => opt.MapFrom(src =>
                    src.FrontStudentIdPicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.FrontStudentIdPicture)}" : null))
                .ForMember(dest => dest.BackStudentIdPicture, opt => opt.MapFrom(src =>
                    src.BackStudentIdPicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.BackStudentIdPicture)}" : null));

            // --- Specific Profile Mappings (for GetMyProfile endpoint) ---
            CreateMap<User, BaseProfileDto>()
             // REMOVE THIS LINE: .Include<Student, GetStudentProfileDto>()
             .Include<Teacher, GetTeacherProfileDto>()
             .Include<Staff, GetStaffProfileDto>()
             .Include<Admin, GetAdminProfileDto>();

            //CreateMap<Student, GetStudentProfileDto>()
            //    .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src =>
            //        src.ProfilePicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.ProfilePicture)}" : null))
            //    .ForMember(dest => dest.FrontStudentIdPicture, opt => opt.MapFrom(src =>
            //        src.FrontStudentIdPicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.FrontStudentIdPicture)}" : null))
            //    .ForMember(dest => dest.BackStudentIdPicture, opt => opt.MapFrom(src =>
            //        src.BackStudentIdPicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.BackStudentIdPicture)}" : null));
            CreateMap<Student, GetStudentProfileDto>()
                // 1. Force it to include the base mapping
                .IncludeBase<User, BaseProfileDto>()

                // 2. Explicitly map the image properties with custom logic (THIS IS FINE)
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src =>
                    src.ProfilePicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.ProfilePicture)}" : null))
                .ForMember(dest => dest.FrontStudentIdPicture, opt => opt.MapFrom(src =>
                    src.FrontStudentIdPicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.FrontStudentIdPicture)}" : null))
                .ForMember(dest => dest.BackStudentIdPicture, opt => opt.MapFrom(src =>
                    src.BackStudentIdPicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.BackStudentIdPicture)}" : null))

                // 3. Explicitly map ALL derived properties
                // Because the Entity properties have `= string.Empty` (are non-nullable at runtime),
                // we should use a simple MapFrom and let the entity's non-nullable nature handle the default value.
                .ForMember(dest => dest.StudentIdNumber, opt => opt.MapFrom(src => src.StudentIdNumber))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course))
                .ForMember(dest => dest.Section, opt => opt.MapFrom(src => src.Section))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.CityMunicipality, opt => opt.MapFrom(src => src.CityMunicipality))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode));

            // The other derived maps should look similar (just with their specific fields):
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

            // This region defines how to map incoming DTOs from client requests into your
            // internal database models for creation or updates.

            // --- Initial User Registration ---
            // Handles the first step of registration, using the minimal RegisterUserDto.
            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => src.Role))
                .Include<RegisterUserDto, Student>()
                .Include<RegisterUserDto, Teacher>()
                .Include<RegisterUserDto, Staff>()
                .Include<RegisterUserDto, Admin>();

            CreateMap<RegisterUserDto, Student>();
            CreateMap<RegisterUserDto, Teacher>();
            CreateMap<RegisterUserDto, Staff>();
            CreateMap<RegisterUserDto, Admin>();

            CreateMap<UpdateStudentProfileDto, Student>()
               // Rule 1: Handle the image properties with specific logic first.
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
               // Rule 2: Handle all other properties with the general "ignore nulls" rule.
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // The other update DTOs are simpler as they don't have images.
            CreateMap<UpdateTeacherProfileDto, Teacher>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateStaffProfileDto, Staff>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateAdminProfileDto, Admin>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            // --- User Profile Updates ---
            CreateMap<UpdateStudentProfileDto, Student>()
                // First, handle all the simple properties that aren't files.
                // This rule applies to everything EXCEPT the properties we define special rules for below.
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateTeacherProfileDto, Teacher>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ... and so on for Staff and Admin ...
            CreateMap<UpdateStaffProfileDto, Staff>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateAdminProfileDto, Admin>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            // Handles the second step where a user completes their profile with detailed information.
            CreateMap<UpdateStudentProfileDto, Student>()
                .ForMember(dest => dest.ProfilePicture, opt => {
                    opt.Condition(src => src.ProfilePicture != null); // Only update if a new picture is provided
                    opt.MapFrom(src => ImageConverterUtils.ConvertIFormFileToByteArray(src.ProfilePicture));
                })
                .ForMember(dest => dest.FrontStudentIdPicture, opt => {
                    opt.Condition(src => src.FrontStudentIdPicture != null);
                    opt.MapFrom(src => ImageConverterUtils.ConvertIFormFileToByteArray(src.FrontStudentIdPicture));
                })
                .ForMember(dest => dest.BackStudentIdPicture, opt => {
                    opt.Condition(src => src.BackStudentIdPicture != null);
                    opt.MapFrom(src => ImageConverterUtils.ConvertIFormFileToByteArray(src.BackStudentIdPicture));
                });

            CreateMap<UpdateTeacherProfileDto, Teacher>();
            CreateMap<UpdateStaffProfileDto, Staff>();
            CreateMap<UpdateAdminProfileDto, Admin>();

            // --- Generic DTO to Model ---
            // Potentially used for generic update operations if needed elsewhere in the application.
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