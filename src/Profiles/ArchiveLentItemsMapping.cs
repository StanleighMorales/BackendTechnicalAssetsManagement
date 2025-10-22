using AutoMapper;
using BackendTechnicalAssetsManagement.src.Classes;
using BackendTechnicalAssetsManagement.src.DTOs.Archive.LentItems;

namespace BackendTechnicalAssetsManagement.src.Profiles
{
    public class ArchiveLentItemsMapping : Profile
    {
        public ArchiveLentItemsMapping()
        {
            // === DTO -> ENTITY MAPPINGS (Receiving data for storage) ===

            // Simple map for reading back into the DTO/Entity structure.
            CreateMap<ArchiveLentItemsDto, ArchiveLentItems>();

            // Map for creating a new Archive record from the Create DTO
            CreateMap<CreateArchiveLentItemsDto, ArchiveLentItems>()
                // FIX: Primary key (Id) should be ignored as the database will generate it.
                .ForMember(dest => dest.Id, opt => opt.Ignore())

                // FIX: Ignore Navigation Properties (Item, User, Teacher)
                // Solves the "String cannot be converted to Object" errors (e.g., "Destination Member: User").
                // The Foreign Keys (ItemId, UserId, TeacherId) are mapped automatically.
                .ForMember(dest => dest.Item, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Teacher, opt => opt.Ignore());


            // === ENTITY -> DTO / ENTITY MAPPINGS (Sending data or restoring) ===

            // Map for Archiving: Active Entity -> Create Archive DTO
            CreateMap<LentItems, CreateArchiveLentItemsDto>()
                // FIX: Explicitly map the active item's ID (src.Id) to the archive's foreign key (dest.LentItemId).
                // Solves the "lentItemId: 00000000-0000..." error during Archiving.
                .ForMember(dest => dest.LentItemId, opt => opt.MapFrom(src => src.Id));

                

            // Map for Restoration: Archive Entity -> Active Entity
            CreateMap<ArchiveLentItems, LentItems>()
                 // FIX: Map the archive's foreign key (src.LentItemId) back to the active item's primary key (dest.Id).
                 // Solves the "Missing type map configuration" error and ensures the restored item keeps its original ID.
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.LentItemId))

                 // FIX: Ignore Navigation Properties for safe conversion.
                 .ForMember(dest => dest.Item, opt => opt.Ignore())
                 .ForMember(dest => dest.User, opt => opt.Ignore())
                 .ForMember(dest => dest.Teacher, opt => opt.Ignore());

            // Map for Reading/Listing Archive DTOs
            CreateMap<ArchiveLentItems, ArchiveLentItemsDto>()
                .ForMember(dest => dest.BarcodeImage, opt => opt.MapFrom(src =>
                    src.BarcodeImage != null ?
                    $"data:image/png;base64,{Convert.ToBase64String(src.BarcodeImage)}" :
                    null));

            // Map for Response after Restoration (Active Entity -> Archive DTO)
            CreateMap<LentItems, ArchiveLentItemsDto>()
                // FIX: The active LentItems entity does not have a LentItemId field. 
                // Ignoring it prevents it from defaulting to "00000000..." in the final JSON response.
                .ForMember(dest => dest.LentItemId, opt => opt.Ignore())
                .ForMember(dest => dest.BarcodeImage, opt => opt.MapFrom(src =>
                    src.BarcodeImage != null ?
                    $"data:image/png;base64,{Convert.ToBase64String(src.BarcodeImage)}" :
                    null));

            // Map for converting a retrieved Archive DTO into a Create DTO (for internal/update use)
            CreateMap<ArchiveLentItems, CreateArchiveLentItemsDto>();
        }
    }
}