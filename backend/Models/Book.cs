namespace bookApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }

        public string? ImageUrl { get; set; }

        public List<ReadBook> Reviews { get; set; } = new();
    }
}
