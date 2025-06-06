namespace Sonare.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int ClipId { get; set; }
        public int UserId { get; set; }
        public string Body { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public Clip? Clip { get; set; } = null!;
        public User? User { get; set; } = null!;
    }
}
