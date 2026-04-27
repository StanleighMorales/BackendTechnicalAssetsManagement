using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.ActivityLog;

namespace BackendTechnicalAssetsManagement.src.Profiles
{
    public class ActivityLogMappingProfile : Profile
    {
        public ActivityLogMappingProfile()
        {
            // ActivityLog entity -> ActivityLogDto
            CreateMap<ActivityLog, ActivityLogDto>();

            // ActivityLog entity -> BorrowLogDto
            CreateMap<ActivityLog, BorrowLogDto>()
                .ForMember(dest => dest.BorrowerUserId, opt => opt.MapFrom(src => src.ActorUserId))
                .ForMember(dest => dest.BorrowerName, opt => opt.MapFrom(src => src.ActorName))
                .ForMember(dest => dest.BorrowerRole, opt => opt.MapFrom(src => src.ActorRole))
                .ForMember(dest => dest.CurrentStatus, opt => opt.MapFrom(src => src.NewStatus ?? string.Empty))
                .ForMember(dest => dest.StudentIdNumber, opt => opt.MapFrom(src =>
                    src.LentItem != null ? src.LentItem.StudentIdNumber : null))
                .ForMember(dest => dest.FrontStudentIdPictureUrl, opt => opt.MapFrom(src =>
                    src.LentItem != null ? src.LentItem.FrontStudentIdPictureUrl : null))
                .ForMember(dest => dest.GuestImageUrl, opt => opt.MapFrom(src =>
                    src.LentItem != null ? src.LentItem.GuestImageUrl : null));
        }
    }
}
