using System.ComponentModel.DataAnnotations;

namespace SMARTLIBRARY.Models
{
    public class ResourceAccessLog
    {
        [Key]
        public int LogId { get; set; }

        public DateTime AccessedAt { get; set; }  // Static date

        [Required, MaxLength(20)]
        public string AccessType { get; set; } = "View"; // View / Download

        // FK
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }

        public string? BookId { get; set; }
        public Book? Book { get; set; }

        public string? ResourceId { get; set; }
        public PdfResource? PdfResource { get; set; }
    }
}
