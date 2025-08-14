using EmployeeProjectTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProjectTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique constraints
            modelBuilder.Entity<Project>()
                .HasIndex(p => p.ProjectCode)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.EmployeeCode)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();

            // One-to-many relationship
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Employees)
                .WithOne(e => e.Project)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // SEED DATA
            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    ProjectId = 1,
                    ProjectCode = "PRJ001",
                    ProjectName = "AI Chatbot Platform",
                    StartDate = new DateTime(2025, 1, 15),
                    EndDate = null,
                    Budget = 500000m
                },
                new Project
                {
                    ProjectId = 2,
                    ProjectCode = "PRJ002",
                    ProjectName = "E-Commerce Website",
                    StartDate = new DateTime(2025, 2, 1),
                    EndDate = null,
                    Budget = 300000m
                }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    EmployeeCode = "EMP001",
                    FullName = "Suresh Kumar",
                    Email = "suresh.kumar@example.com",
                    Designation = "Software Engineer",
                    Salary = 75000m,
                    ProjectId = 1
                },
                new Employee
                {
                    EmployeeId = 2,
                    EmployeeCode = "EMP002",
                    FullName = "Priya Sharma",
                    Email = "priya.sharma@example.com",
                    Designation = "Project Manager",
                    Salary = 120000m,
                    ProjectId = 1
                },
                new Employee
                {
                    EmployeeId = 3,
                    EmployeeCode = "EMP003",
                    FullName = "Rajesh Gupta",
                    Email = "rajesh.gupta@example.com",
                    Designation = "UI/UX Designer",
                    Salary = 65000m,
                    ProjectId = 2
                }
            );
        }
    }
}
