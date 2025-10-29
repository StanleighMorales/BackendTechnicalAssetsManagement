using System.ComponentModel.DataAnnotations;
using static BackendTechnicalAssetsManagement.src.Classes.Enums;

namespace BackendTechnicalAssetsManagement.src.DTOs.Item
{
    public class UpdateItemsDto
    {
        public string? SerialNumber { get; set; } 
    
        public IFormFile? Image { get; set; } 
    
        public string? ItemName { get; set; } 
    
        public string? ItemType { get; set; } 
    
        public string? ItemModel { get; set; }
    
        public string? ItemMake { get; set; } 
    
        public string? Description { get; set; }
    
        public ItemCategory? Category { get; set; } 
    
        public ItemCondition? Condition { get; set; }

        public ItemStatus? Status { get; set; }

    }
}
