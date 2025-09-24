namespace SMARTLIBRARY.DTOs.Requests
{
    public class NotificationRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string RecipientId { get; set; } = string.Empty;
    }
}
