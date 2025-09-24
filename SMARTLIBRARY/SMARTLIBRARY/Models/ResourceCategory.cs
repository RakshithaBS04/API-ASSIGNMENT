using System.ComponentModel.DataAnnotations;

namespace SMARTLIBRARY.Models
{
    public class ResourceCategory
    {
        [Key]
        public string CategoryId { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        // Relationships
        public ICollection<Book> Books { get; set; } = new List<Book>();
        public ICollection<PdfResource> PdfResources { get; set; } = new List<PdfResource>();
    }
}
