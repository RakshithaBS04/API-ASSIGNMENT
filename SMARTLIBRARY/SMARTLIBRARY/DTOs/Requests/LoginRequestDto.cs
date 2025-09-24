using System.ComponentModel.DataAnnotations;

namespace SMARTLIBRARY.DTOs.Requests
{
    public class LoginRequestDto
    {
        [Required]
        public string UserId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
