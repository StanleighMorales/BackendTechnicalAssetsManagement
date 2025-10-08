using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.Archive;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.Profiles
{
    public class ArchiveItemsMappingProfile : Profile
    {
        public ArchiveItemsMappingProfile()
        {
            // Entity -> DTO
            CreateMap<ArchiveItems, ArchiveItemsDto>();
            CreateMap<ArchiveItems, CreateArchiveItemsDto>();
            CreateMap<ArchiveItems, UpdateArchiveItemsDto>();

            // This is the map for the restore functionality
            CreateMap<ArchiveItems, Item>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ItemId));
            // The Category and Condition properties will be mapped automatically
            // because they have the same name and type in both classes.
            // The incorrect Enum.Parse calls have been removed.
       

            // DTO -> Entity
            CreateMap<ArchiveItemsDto, ArchiveItems>();
            CreateMap<CreateArchiveItemsDto, ArchiveItems>()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId));
            CreateMap<UpdateArchiveItemsDto, ArchiveItems>()
                 .ForMember(dest => dest.ItemId, opt => opt.Ignore())
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }


    }
}
