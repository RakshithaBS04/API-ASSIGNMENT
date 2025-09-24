using System.ComponentModel.DataAnnotations;

namespace SMARTLIBRARY.DTOs.Requests
{
    public class BookRequestDto
    {

        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty; // PDF
        public string? ImageUrl { get; set; } // Cover image

        [Required]
        public string? CategoryId { get; set; }
        public string UploadedById { get; set; } = string.Empty;
    }
}
