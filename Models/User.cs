namespace Sonare.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public ICollection<Clip> Clips { get; set; } = new List<Clip>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<ClipCollaborator> ClipCollaborations { get; set; } = new List<ClipCollaborator>();
    }
}
