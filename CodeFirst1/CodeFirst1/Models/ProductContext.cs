using Microsoft.EntityFrameworkCore;

namespace CodeFirst1.Models
{
    public class ProductContext:DbContext
    {
        //List of Products for product entity refeered to as DbSet
        public DbSet<Product> Products { get; set; }

        //Constructor  - called during exection
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }
        //called during migration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=DESKTOP-00GRLI4;Database=Worker;Integrated Security=True;TrustServerCertificate=True;");
        }

        public DbSet<Category> Categories { get; set; }


    }
}
