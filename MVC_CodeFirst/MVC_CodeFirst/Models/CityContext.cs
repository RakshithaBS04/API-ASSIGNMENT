using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MVC_CodeFirst.Models
{
    public class CityContext : DbContext
    {
        public CityContext(DbContextOptions<CityContext> options) : base(options)
        {
        }
        public DbSet<City> Cities { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-00GRLI4;Database=weather_forecast;Integrated Security=True;TrustServerCertificate=True;");
        }
        
    }
}

