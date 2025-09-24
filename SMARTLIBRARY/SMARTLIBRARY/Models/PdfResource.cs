using System.ComponentModel.DataAnnotations;

namespace SMARTLIBRARY.Models
{
    public class PdfResource
    {
        [Key]
        public string ResourceId { get; set; } = null!;

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        public string FilePath { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string ResourceType { get; set; } = "IEEE Paper";

        public bool IsActive { get; set; } = true;
        public DateTime UploadedAt { get; set; }  // Static date

        // FK
        public string CategoryId { get; set; } = null!;
        public ResourceCategory? Category { get; set; }

        public string UploadedById { get; set; } = string.Empty;
        public User? UploadedBy { get; set; }

        // Access logs
        public ICollection<ResourceAccessLog> AccessLogs { get; set; } = new List<ResourceAccessLog>();
    }
}
