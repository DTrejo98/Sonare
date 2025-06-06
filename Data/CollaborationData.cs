using Sonare.Models;

public static class CollaborationData
{
    public static List<Collaboration> Collaborations = new()
    {
        new() { Id = 1, OriginalClipId = 1, ResponseClipId = 3, CreatedAt = new DateTime(2024, 6, 4) },
        new() { Id = 2, OriginalClipId = 2, ResponseClipId = 1, CreatedAt = new DateTime(2024, 6, 5) }
    };
}
