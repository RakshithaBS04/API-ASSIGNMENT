namespace SMARTLIBRARY.DTOs.Responses
{
    public class AccessLogResponseDto
    {
        public int LogId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string? BookId { get; set; }
        public string? ResourceId { get; set; }
        public string AccessType { get; set; } = string.Empty; // View / Download
        public DateTime AccessedAt { get; set; }
    }
}
