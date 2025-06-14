using Sonare.Models;

public static class CommentData
{
    public static List<Comment> Comments = new()
    {
        new() { Id = 1, ClipId = 1, UserId = 2, Body = "Love the vibe on this!", CreatedAt = new DateTime(2024, 5, 30) },
        new() { Id = 2, ClipId = 2, UserId = 3, Body = "These drums hit hard. 🔥", CreatedAt = new DateTime(2024, 6, 1) },
        new() { Id = 3, ClipId = 3, UserId = 1, Body = "Very cinematic feel, nice job!", CreatedAt = new DateTime(2024, 6, 3) }
    };
}