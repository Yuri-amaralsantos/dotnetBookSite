namespace bookApi.Models
{
    public class ReadBook
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

     
        public string GoogleBookId { get; set; } = string.Empty;

        public int? Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime ReadAt { get; set; } = DateTime.UtcNow;
    }
}
