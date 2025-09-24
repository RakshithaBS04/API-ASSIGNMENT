namespace SMARTLIBRARY.DTOs.Responses
{
    public class PdfResourceResponseDto
    {
        public string? ResourceId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string ResourceType { get; set; } = "IEEE Paper";
        public string? CategoryId { get; set; }
        public string UploadedById { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
