using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.Archive.Items;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.Profiles
{
    public class ArchiveItemsMappingProfile : Profile
    {
        public ArchiveItemsMappingProfile()
        {
            // Entity -> DTO
            CreateMap<ArchiveItems, ArchiveItemsDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src =>
                    src.Image != null && src.ImageMimeType != null ?
                    $"data:{src.ImageMimeType};base64,{Convert.ToBase64String(src.Image)}" :
                    null))
                // **NEW MAPPING for Barcode Image** (This remains correct)
                .ForMember(dest => dest.BarcodeImage, opt => opt.MapFrom(src =>
                    src.BarcodeImage != null ?
                    $"data:image/png;base64,{Convert.ToBase64String(src.BarcodeImage)}" :
                    null))

                // FIX: Map String (Entity) to Enum (DTO) (This remains correct)
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => Enum.Parse<ItemCategory>(src.Category.ToString())))
                .ForMember(dest => dest.Condition, opt => opt.MapFrom(src => Enum.Parse<ItemCondition>(src.Condition.ToString())));

            CreateMap<ArchiveItems, CreateArchiveItemsDto>();
            CreateMap<ArchiveItems, UpdateArchiveItemsDto>();

            // This is the map for the restore functionality
            CreateMap<ArchiveItems, Item>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ItemId));
            // The Category and Condition properties will be mapped automatically
            // because they have the same name and type in both classes 
            // and the Item entity expects Enums.

            // DTO -> Entity
            CreateMap<ArchiveItemsDto, ArchiveItems>();
            CreateMap<CreateArchiveItemsDto, ArchiveItems>()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId));
            CreateMap<UpdateArchiveItemsDto, ArchiveItems>()
                 .ForMember(dest => dest.ItemId, opt => opt.Ignore())
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Item, CreateArchiveItemsDto>()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
                .ForMember(dest => dest.Condition, opt => opt.MapFrom(src => src.Condition.ToString()))
                .ForMember(dest => dest.Barcode, opt => opt.MapFrom(src => src.Barcode))
                .ForMember(dest => dest.BarcodeImage, opt => opt.MapFrom(src => src.BarcodeImage))
                .ForMember(dest => dest.ImageMimeType, opt => opt.MapFrom(src => src.ImageMimeType));

        }


    }
}