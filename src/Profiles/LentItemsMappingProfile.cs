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
                .ForMember(dest => dest.BarcodeImage, opt => opt.MapFrom(src =>
                    src.BarcodeImage != null ?
                    $"data:image/png;base64,{Convert.ToBase64String(src.BarcodeImage)}" :
                    null))
                .ForMember(dest => dest.FrontStudentIdPicture, opt => opt.MapFrom(src =>
                    src.User != null && src.User.GetType() == typeof(Student) && ((Student)src.User).FrontStudentIdPicture != null ?
                    $"data:image/png;base64,{Convert.ToBase64String(((Student)src.User).FrontStudentIdPicture)}" :
                    null));
            // DTO -> Entity (for create)
            CreateMap<CreateLentItemDto, LentItems>()
                .ForMember(dest => dest.BarcodeImage, opt => opt.Ignore())
                .ForMember(dest => dest.LentAt, opt => opt.Ignore()) // LentAt should only be set when status becomes Borrowed
                .ForMember(dest => dest.ItemName, opt => opt.Ignore()) // ItemName must be looked up in the service layer
                .ForMember(dest => dest.BorrowerFullName, opt => opt.Ignore()) // Denormalized fields are set in the service layer
                .ForMember(dest => dest.BorrowerRole, opt => opt.Ignore())
                .ForMember(dest => dest.StudentIdNumber, opt => opt.Ignore())
                .ForMember(dest => dest.TeacherFullName, opt => opt.Ignore());

            // DTO -> Entity (for update)
            CreateMap<UpdateLentItemDto, LentItems>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ScanLentItemDto , LentItems>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateLentItemsForGuestDto, LentItems>();
        }
    }
}
