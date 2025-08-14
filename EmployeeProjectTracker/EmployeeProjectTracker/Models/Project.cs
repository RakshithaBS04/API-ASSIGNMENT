using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeProjectTracker.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required, StringLength(10)]
        public string ProjectCode { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string ProjectName { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Budget { get; set; }

        public ICollection<Employee>? Employees { get; set; } = new List<Employee>();
    }
}
