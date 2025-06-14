namespace Sonare.Models
{
    public class Collaboration
    {
        public int Id { get; set; }
        public int OriginalClipId { get; set; }
        public int ResponseClipId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Clip? OriginalClip { get; set; } = null!;
        public Clip? ResponseClip { get; set; } = null!;
    }
}
