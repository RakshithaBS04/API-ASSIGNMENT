using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public string Category { get; set; }     //It was used before relating 2 models

        public int catId { get; set; }

        //Link 2 models by setting navigation property
        [ForeignKey("catId")]
        public Category Category { get; set; }


    }
}
