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
                .ForMember(dest => dest.TeacherFullName,
                    opt => opt.MapFrom(src =>
                        // Priority 1: If the related Teacher object is loaded, use it.
                        src.Teacher != null ? $"{src.Teacher.FirstName} {src.Teacher.LastName}"
                        // Priority 2 (Fallback): Otherwise, use the name we stored directly in the table.
                        : src.TeacherFullName))

                .ForMember(dest => dest.ItemName, // <--- TARGET MEMBER
                                                  // The expression must return a 'string'
                    opt => opt.MapFrom(src =>
                        // 1. Check if the navigation property was loaded
                        src.Item != null
                            // 2. If it was loaded, use the ItemName from the full object
                            ? src.Item.ItemName
                            // 3. Fallback: If not, use the denormalized string property
                            : src.ItemName));
            // DTO -> Entity (for create)
            CreateMap<CreateLentItemDto, LentItems>();

            // DTO -> Entity (for update)
            CreateMap<UpdateLentItemDto, LentItems>()
                .ForMember(dest => dest.ItemId, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateLentItemsForGuestDto, LentItems>();
        }
    }
}
