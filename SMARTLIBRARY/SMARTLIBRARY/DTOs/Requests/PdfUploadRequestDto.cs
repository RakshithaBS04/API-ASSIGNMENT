using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SMARTLIBRARY.DTOs.Requests
{
    public class PdfUploadRequestDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string CategoryId { get; set; } = string.Empty;

        [Required]
        public string UploadedById { get; set; } = string.Empty;

        [Required]
        public IFormFile FilePath { get; set; } = null!;
    }
}
