using System.ComponentModel.DataAnnotations;

namespace SMARTLIBRARY.DTOs.Requests
{
    public class PdfResourceRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string ResourceType { get; set; } = "IEEE Paper";

        [Required]
        public string? CategoryId { get; set; }
        public string UploadedById { get; set; } = string.Empty;
    }
}
