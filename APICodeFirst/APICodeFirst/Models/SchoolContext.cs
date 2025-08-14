using Microsoft.EntityFrameworkCore;
using APICodeFirst.Models;

namespace APICodeFirst.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        //Seeding of data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API configuration
            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryId);
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Students)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId);

            modelBuilder.Entity<Student>()
                .HasKey(s => s.StudentId);
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Category) 
                .WithMany(c => c.Students) 
                .HasForeignKey(s => s.CategoryId);

            // Add Dummy data
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Mathematics" },
                new Category { CategoryId = 2, Name = "Science" },
                new Category { CategoryId = 3, Name = "History" }
            );
            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, Name = "John Doe", Age = 20, CategoryId = 1 },
                new Student { StudentId = 2, Name = "Jane Smith", Age = 22, CategoryId = 2 },
                new Student { StudentId = 3, Name = "Sam Brown", Age = 19, CategoryId = 3 }
            );
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Student> Students { get; set; }
    }


}
