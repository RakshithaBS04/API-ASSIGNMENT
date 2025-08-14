namespace APICodeFirst_Interface.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }

        // Foreign Key
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
