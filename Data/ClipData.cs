using Sonare.Models;

public static class ClipData
{
    public static List<Clip> Clips = new()
    {
        new() { Id = 1, UserId = 1, Title = "Dreamy Synths", MediaUrl = "/media/dreamy.mp3", Description = "A smooth synthwave track", IsFinalMix = true, CreatedAt = new DateTime(2024, 5, 29) },
        new() { Id = 2, UserId = 2, Title = "Hip-Hop Drums", MediaUrl = "/media/hiphop-drums.mp3", Description = "Punchy drum loop for hip-hop", IsFinalMix = false, CreatedAt = new DateTime(2024, 5, 31) },
        new() { Id = 3, UserId = 3, Title = "Ambient Layers", MediaUrl = "/media/ambient.mp3", Description = "Layered ambient textures", IsFinalMix = true, CreatedAt = new DateTime(2024, 6, 2) }
    };
}
