using AutoMapper;
using BackendTechnicalAssetsManagement.src.DTOs.User;
using BackendTechnicalAssetsManagement.src.Models;
using BackendTechnicalAssetsManagement.src.Models.DTOs.Users;

namespace BackendTechnicalAssetsManagement.src.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // Base mappings
            //From Model to DTO
            CreateMap<User, UserDto>()
                .Include<Teacher, TeacherDto>()
                .Include<Student, StudentDto>()
                .Include<Staff, StaffDto>()
                .Include<Manager, ManagerDto>()
                .Include<Admin, AdminDto>();


            //after including in the base mapping what will happen is it will go to the map that is included so make sure to add it (CreateMap)
            CreateMap<Staff, StaffDto>();
            CreateMap<Teacher, TeacherDto>();
            CreateMap<Manager, ManagerDto>();
            CreateMap<Admin, AdminDto>();
            // Derived mappings
            // From DTO to Model
            CreateMap<UserDto, User>()
                .Include<TeacherDto, Teacher>()
                .Include<StudentDto, Student>()
                .Include<StaffDto, Staff>()
                .Include<ManagerDto, Manager>()
                .Include<AdminDto, Admin>();
            //this helps us map the base class properties automatically
            //instead of mapping each property one by one
            //e.g newUser { Id = dto.Id, LastName = dto.LastName, ... }
            // we can just do this 
            //  var newUser = _mapper.Map<User>(createUserDto);

            CreateMap<StaffDto, Staff>();
            CreateMap<TeacherDto, Teacher>();

            CreateMap<RegisterStaffDto, Staff>()
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => Enums.UserRole.Staff));
            CreateMap<RegisterTeacherDto, Teacher>()
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => Enums.UserRole.Teacher));
            CreateMap<RegisterStudentDto, Student>()
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => Enums.UserRole.Student))
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.FrontStudentIdPictureUrl, opt => opt.Ignore())
                .ForMember(dest => dest.BackStudentIdPictureUrl, opt => opt.Ignore());
            CreateMap<RegisterManagerDto, Manager>()
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => Enums.UserRole.Manager));
            CreateMap<RegisterAdminDto, Admin>()
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => Enums.UserRole.Admin));

            CreateMap<User, BaseProfileDto>();
            CreateMap<Student, GetStudentProfileDto>();
            CreateMap<Teacher, GetTeacherProfileDto>();
            CreateMap<Staff, GetStaffProfileDto>();
            CreateMap<Manager, GetManagerProfileDto>();
            CreateMap<Admin, GetAdminProfileDto>();
        }
    }
}
