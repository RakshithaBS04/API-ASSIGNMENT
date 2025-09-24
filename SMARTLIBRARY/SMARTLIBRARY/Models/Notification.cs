using System.ComponentModel.DataAnnotations;

namespace SMARTLIBRARY.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        public string Message { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }  // Static date
        public bool IsRead { get; set; } = false;

        // FK
        public string RecipientId { get; set; } = string.Empty;
        public User? Recipient { get; set; }
    }
}
