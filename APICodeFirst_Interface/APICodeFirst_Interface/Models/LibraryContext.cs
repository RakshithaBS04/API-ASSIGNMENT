using APICodeFirst_Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace APICodeFirst_Interface.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Author Config
            modelBuilder.Entity<Author>()
                .HasKey(a => a.AuthorId);

            modelBuilder.Entity<Author>()
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Book Config
            modelBuilder.Entity<Book>()
                .HasKey(b => b.BookId);

            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            // Seed Data
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, Name = "Ram", BirthDate = new DateTime(1903, 6, 25) },
                new Author { AuthorId = 2, Name = "John", BirthDate = new DateTime(1775, 12, 16) }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "1984", PublishedDate = new DateTime(1949, 6, 8), AuthorId = 1 },
                new Book { BookId = 2, Title = "Pride and Prejudice", PublishedDate = new DateTime(1813, 1, 28), AuthorId = 2 }
            );
        }
    }
}
