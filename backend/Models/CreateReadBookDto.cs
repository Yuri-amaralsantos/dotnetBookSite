namespace bookApi.DTOs
{
    public class CreateReadBookDto
    {
        public string GoogleBookId { get; set; } = string.Empty;
        public int? Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
