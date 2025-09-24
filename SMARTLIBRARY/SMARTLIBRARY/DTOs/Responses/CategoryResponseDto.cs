namespace SMARTLIBRARY.DTOs.Responses
{
    public class CategoryResponseDto
    {
        public string? CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
