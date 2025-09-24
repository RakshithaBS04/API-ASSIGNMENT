namespace SMARTLIBRARY.DTOs.Requests
{
    public class RegisterUserRequestDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty; // "Student" / "Faculty"
        public string? ImageUrl { get; set; } // Optional profile image
    }
}
