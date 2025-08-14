using System.ComponentModel.DataAnnotations;

namespace CodeFirst1.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; } //Primary Key   
        public string? CategoryName { get; set; }
        
        //Mapped by navigation property
        public IList<Product> Products { get; set; }
    }
}
