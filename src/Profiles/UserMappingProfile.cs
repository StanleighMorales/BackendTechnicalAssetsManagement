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
                .Include<Student, GetStudentProfileDto>()
                .Include<Teacher, GetTeacherProfileDto>()
                .Include<Staff, GetStaffProfileDto>()
                .Include<Admin, GetAdminProfileDto>();

            CreateMap<Student, GetStudentProfileDto>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src =>
                    src.ProfilePicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.ProfilePicture)}" : null))
                .ForMember(dest => dest.FrontStudentIdPicture, opt => opt.MapFrom(src =>
                    src.FrontStudentIdPicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.FrontStudentIdPicture)}" : null))
                .ForMember(dest => dest.BackStudentIdPicture, opt => opt.MapFrom(src =>
                    src.BackStudentIdPicture != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.BackStudentIdPicture)}" : null));

            CreateMap<Teacher, GetTeacherProfileDto>();
            CreateMap<Staff, GetStaffProfileDto>();
            CreateMap<Admin, GetAdminProfileDto>();

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

            // --- User Profile Updates ---
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