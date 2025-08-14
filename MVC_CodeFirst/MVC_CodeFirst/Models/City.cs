using System.ComponentModel.DataAnnotations;

namespace MVC_CodeFirst.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

    }
}
