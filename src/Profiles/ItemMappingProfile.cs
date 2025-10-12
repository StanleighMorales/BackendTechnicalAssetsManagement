using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.Archive;
using BackendTechnicalAssetsManagement.src.DTOs.Item;
using BackendTechnicalAssetsManagement.src.Utils;
using TechnicalAssetManagementApi.Dtos.Item;

namespace BackendTechnicalAssetsManagement.src.Profiles
{
    public class ItemMappingProfile : Profile
    {
        public ItemMappingProfile()
        {
            //Reads Data from CreateItemDto and UpdateItemDto and maps to Item entity
            CreateMap<Item, ItemDto>()

                .ForMember(dest => dest.Image, opt => opt.MapFrom(src =>
                    src.Image != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.Image)}" : null))
                .ForMember(dest => dest.BarcodeImage, opt => opt.MapFrom(src =>
                    src.BarcodeImage != null ?
                    $"data:image/png;base64,{Convert.ToBase64String(src.BarcodeImage)}" :
                    null));


            CreateMap<ItemDto, Item>()
                .ForMember(dest => dest.BarcodeImage, opt => opt.Ignore());
            // When mapping from UpdateItemDto to Item, ignore the Image property
            CreateMap<CreateItemsDto, Item>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ImageConverterUtils.ConvertIFormFileToByteArray(src.Image)));

            CreateMap<UpdateItemsDto, Item>()
                 // Add the explicit mapping for Image to handle the conversion and the null check
                 .ForMember(dest => dest.Image, opt => opt.Ignore())
                      .ForMember(dest => dest.SerialNumber, opt => opt.Ignore())

                 // *** ADD THESE LINES ***
                 .ForMember(dest => dest.Barcode, opt => opt.Ignore())
                 .ForMember(dest => dest.BarcodeImage, opt => opt.Ignore())
                 // Keep the AllMembers condition for all other properties 
                 // (it will apply to other properties that should only update if not null)
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Item, CreateArchiveItemsDto>()
                // Explicitly map the 'Id' from the source (Item) to 'ItemId' in the destination (DTO)
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.Id))

                // Convert the 'Category' enum from the source to a string in the destination
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))

                // Convert the 'Condition' enum from the source to a string in the destination
                .ForMember(dest => dest.Condition, opt => opt.MapFrom(src => src.Condition.ToString()));

        }
    }
}
