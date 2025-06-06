namespace Sonare.Models
{
    public class Clip
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string MediaUrl { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsFinalMix { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Collaboration> OriginalCollaborations { get; set; } = new List<Collaboration>();
        public ICollection<Collaboration> ResponseCollaborations { get; set; } = new List<Collaboration>();
        public ICollection<ClipCollaborator> ClipCollaborators { get; set; } = new List<ClipCollaborator>();
    }
}
