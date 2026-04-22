using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTechnicalAssetsManagement.src.Classes
{
    /// <summary>
    /// Represents an RFID card in the system.
    /// This table stores all available RFID cards before they are assigned to students.
    /// </summary>
    [Table("Rfids")]
    public class Rfid
    {
        [Key]
        [Column("RfidUid")]
        public string RfidUid { get; set; } = string.Empty;  // The actual RFID UID from physical card (Primary Key)
        
        [Column("RfidCode")]
        public string RfidCode { get; set; } = string.Empty; // Human-readable code
    }
}
