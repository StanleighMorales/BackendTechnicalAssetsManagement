using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs;
using BackendTechnicalAssetsManagement.src.DTOs.LentItems;

namespace BackendTechnicalAssetsManagement.src.Profiles
{
    public class LentItemsMappingProfile : Profile
    {
        public LentItemsMappingProfile()
        {
            // Entity -> DTO
            CreateMap<LentItems, LentItemsDto>()
                //.ForMember(dest => dest.BorrowerFullName,
                //    opt => opt.MapFrom(src => src.User != null
                //        ? $"{src.User.FirstName} {src.User.LastName}"
                //        : string.Empty))
                //.ForMember(dest => dest.BorrowerRole,
                //    opt => opt.MapFrom(src => src.User != null
                //        ? src.User.UserRole.ToString()
                //        : string.Empty))
                //.ForMember(dest => dest.TeacherFullName,
                //    opt => opt.MapFrom(src => src.Teacher != null
                //        ? $"{src.Teacher.FirstName} {src.Teacher.LastName}"
                //        : null));
                .ForMember(dest => dest.TeacherFullName,
                    opt => opt.MapFrom(src =>
                        // Priority 1: If the related Teacher object is loaded, use it.
                        src.Teacher != null ? $"{src.Teacher.FirstName} {src.Teacher.LastName}"
                        // Priority 2 (Fallback): Otherwise, use the name we stored directly in the table.
                        : src.TeacherFullName
                    ));

            // DTO -> Entity (for create)
            CreateMap<CreateLentItemDto, LentItems>();

            // DTO -> Entity (for update)
            CreateMap<UpdateLentItemDto, LentItems>();

            CreateMap<CreateLentItemsForGuestDto, LentItems>();
        }
    }
}
