namespace bookApi.Models
{
    public class ReadBook
    {
        public int Id { get; set; }

       
        public int UserId { get; set; } 
        public User User { get; set; } = null!;

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

     
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime ReadAt { get; set; } = DateTime.UtcNow;
    }
}
