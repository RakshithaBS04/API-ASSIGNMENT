using System.ComponentModel.DataAnnotations;

namespace SMARTLIBRARY.Models
{
    public class Book
    {
        [Key]
        public string BookId { get; set; } = null!;

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Author { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? ISBN { get; set; }

        [Required, MaxLength(500)]
        public string FilePath { get; set; } = string.Empty; // PDF path

        [MaxLength(250)]
        public string? ImageUrl { get; set; } // Cover image

        public bool IsActive { get; set; } = true;
        public DateTime UploadedAt { get; set; }  // Static date will be set manually

        // FK
        public string CategoryId { get; set; } = null!;
        public ResourceCategory? Category { get; set; }

        public string UploadedById { get; set; } = string.Empty;
        public User? UploadedBy { get; set; }

        // Access logs
        public ICollection<ResourceAccessLog> AccessLogs { get; set; } = new List<ResourceAccessLog>();
    }
}
