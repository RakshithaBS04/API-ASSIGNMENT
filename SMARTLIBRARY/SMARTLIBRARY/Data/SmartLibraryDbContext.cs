using Microsoft.EntityFrameworkCore;
using SMARTLIBRARY.Models;

namespace SMARTLIBRARY.Data
{
    public class SmartLibraryDbContext : DbContext
    {
        public SmartLibraryDbContext(DbContextOptions<SmartLibraryDbContext> options) : base(options) { }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<PdfResource> PdfResources { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ResourceCategory> ResourceCategories { get; set; }
        public DbSet<ResourceAccessLog> ResourceAccessLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User - Role (Many-to-One)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Book - Category (Many-to-One)
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Book - User (Many-to-One)
            modelBuilder.Entity<Book>()
                .HasOne(b => b.UploadedBy)
                .WithMany(u => u.UploadedBooks)
                .HasForeignKey(b => b.UploadedById)
                .OnDelete(DeleteBehavior.Restrict);

            // PdfResource - Category (Many-to-One)
            modelBuilder.Entity<PdfResource>()
                .HasOne(p => p.Category)
                .WithMany(c => c.PdfResources)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // PdfResource - User (Many-to-One)
            modelBuilder.Entity<PdfResource>()
                .HasOne(p => p.UploadedBy)
                .WithMany(u => u.UploadedPdfs)
                .HasForeignKey(p => p.UploadedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Notification - User (Many-to-One)
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Recipient)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.RecipientId)
                .OnDelete(DeleteBehavior.Cascade);

            // ResourceAccessLog - User (Many-to-One)
            modelBuilder.Entity<ResourceAccessLog>()
                .HasOne(l => l.User)
                .WithMany(u => u.AccessLogs)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ResourceAccessLog - Book (Optional Many-to-One)
            modelBuilder.Entity<ResourceAccessLog>()
                .HasOne(l => l.Book)
                .WithMany(b => b.AccessLogs)
                .HasForeignKey(l => l.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            // ResourceAccessLog - PdfResource (Optional Many-to-One)
            modelBuilder.Entity<ResourceAccessLog>()
                .HasOne(l => l.PdfResource)
                .WithMany(p => p.AccessLogs)
                .HasForeignKey(l => l.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);


            // Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin", Description = "System administrator" },
                new Role { RoleId = 2, RoleName = "Librarian", Description = "Manages library resources" },
                new Role { RoleId = 3, RoleName = "Student", Description = "Student user" },
                new Role { RoleId = 4, RoleName = "Faculty", Description = "Faculty user" }
            );

            // Users (with static images)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = "ADM001",
                    FullName = "Super Admin",
                    Email = "admin@smartlib.com",
                    Password = "admin123",
                    ImageUrl = "/Images/admin.jpg",
                    RoleId = 1,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 9, 22)
                },
                new User
                {
                    UserId = "LIB001",
                    FullName = "Main Librarian",
                    Email = "librarian@smartlib.com",
                    Password = "lib123",
                    ImageUrl = "/Images/librarian.png",
                    RoleId = 2,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 9, 22)
                },
                new User
                {
                    UserId = "KAS001",
                    FullName = "Test Student",
                    Email = "student@smartlib.com",
                    Password = "stud123",
                    ImageUrl = "/Images/student.png",
                    RoleId = 3,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 9, 22)
                },
                new User
                {
                    UserId = "KAF001",
                    FullName = "Test Faculty",
                    Email = "faculty@smartlib.com",
                    Password = "fac123",
                    ImageUrl = "/Images/faculty.jpg",
                    RoleId = 4,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 9, 22)
                }
            );

            // Categories
            modelBuilder.Entity<ResourceCategory>().HasData(
                new ResourceCategory { CategoryId = "C1", Name = "Science", Description = "Science related resources", IsActive = true },
                new ResourceCategory { CategoryId = "C2", Name = "Technology", Description = "Technology related resources", IsActive = true }
            );

            // Books
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = "B1",
                    Title = "Physics Fundamentals",
                    Author = "Dr. Newton",
                    ISBN = "123456789",
                    FilePath = "/pdfs/books/physics.pdf",
                    ImageUrl = "/Images/books/physics.png",
                    CategoryId = "C1",
                    UploadedById = "LIB001",
                    UploadedAt = new DateTime(2025, 9, 20),
                    IsActive = true
                }
            );

            // PdfResources (IEEE Papers)
            modelBuilder.Entity<PdfResource>().HasData(
                new PdfResource
                {
                    ResourceId = "R1",
                    Title = "AI Research Paper",
                    FilePath = "/pdfs/papers/ai_paper.pdf",
                    ResourceType = "IEEE Paper",
                    CategoryId = "C2",
                    UploadedById = "KAF001",
                    UploadedAt = new DateTime(2025, 9, 21),
                    IsActive = true
                }
            );

            // Notifications
            modelBuilder.Entity<Notification>().HasData(
                new Notification
                {
                    NotificationId = 1,
                    Title = "Welcome",
                    Message = "Welcome to Smart Library System!",
                    RecipientId = "KAS001",
                    CreatedAt = new DateTime(2025, 9, 22),
                    IsRead = false
                }
            );
        }
    }
}
