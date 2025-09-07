using AutoMapper;
using BackendTechnicalAssetsManagement.src.DTOs.Item;
using BackendTechnicalAssetsManagement.src.Models;
using TechnicalAssetManagementApi.Dtos.Item;

namespace BackendTechnicalAssetsManagement.src.Profiles
{
    public class ItemMappingProfile : Profile
    {
        public ItemMappingProfile()
        {
            //Reads Data from CreateItemDto and UpdateItemDto and maps to Item entity
            CreateMap<Item, ItemDto>();
            // When mapping from UpdateItemDto to Item, ignore the Image property
            CreateMap<CreateItemDto, Item>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<UpdateItemDto, Item>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}
