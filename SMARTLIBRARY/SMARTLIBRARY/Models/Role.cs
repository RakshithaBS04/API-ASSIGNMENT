using System.ComponentModel.DataAnnotations;

namespace SMARTLIBRARY.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required, MaxLength(50)]
        public string RoleName { get; set; } = null!;

        [MaxLength(250)]
        public string? Description { get; set; }

        // Navigation
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
