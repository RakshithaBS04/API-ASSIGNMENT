using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeProjectTracker.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required, StringLength(8)]
        public string EmployeeCode { get; set; } = string.Empty;

        [Required, StringLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Designation { get; set; } = string.Empty;

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public Project? Project { get; set; }
    }
}
