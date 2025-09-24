namespace SMARTLIBRARY.DTOs.Responses
{
    public class BookResponseDto
    {
        public string? BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? CategoryId { get; set; }
        public string UploadedById { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
