using System.ComponentModel.DataAnnotations;

namespace SMARTLIBRARY.Models
{
    public class User
    {
        [Key]
        [MaxLength(20)]
        public string UserId { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Password { get; set; } = string.Empty; // Plain text

        [MaxLength(250)]
        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }  // Static date

        // FK
        public int RoleId { get; set; }
        public Role? Role { get; set; }

        // Navigation
        public ICollection<Book> UploadedBooks { get; set; } = new List<Book>();
        public ICollection<PdfResource> UploadedPdfs { get; set; } = new List<PdfResource>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<ResourceAccessLog> AccessLogs { get; set; } = new List<ResourceAccessLog>();
    }
}
