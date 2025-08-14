namespace APICodeFirst.Models
{
    public class Student
    {
        public int StudentId { get; set; } // Primary Key
        public string Name { get; set; }
        public int Age { get; set; }

        // Foreign key
        public int CategoryId { get; set; }

        // Navigation property
        public Category? Category { get; set; }
    }
}
