using AutoMapper;
using BackendTechnicalAssetsManagement.src.DTOs.Item;
using BackendTechnicalAssetsManagement.src.Models;
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
                    src.Image != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(src.Image)}" : null));

            // When mapping from UpdateItemDto to Item, ignore the Image property
            CreateMap<CreateItemDto, Item>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ImageConverterUtils.ConvertIFormFileToByteArray(src.Image)));

            CreateMap<UpdateItemDto, Item>();
               
        }
    }
}
