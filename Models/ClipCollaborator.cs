namespace Sonare.Models
{
    public class ClipCollaborator
    {
        public int ClipId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; } = null!;
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }

        public Clip? Clip { get; set; } = null!;
        public User? User { get; set; } = null!;
    }
}
