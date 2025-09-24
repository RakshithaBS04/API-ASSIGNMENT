namespace SMARTLIBRARY.DTOs.Responses
{
    public class AuthResponseDto
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty; // JWT token
    }
}
