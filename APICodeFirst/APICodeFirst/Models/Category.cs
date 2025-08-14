namespace APICodeFirst.Models
{
    public class Category
    {
        public int CategoryId { get; set; } // Primary Key
        public string Name { get; set; }

        // Navigation property - One Category can have many Students
        public List<Student>? Students { get; set; }
    }
}
