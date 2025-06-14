using Sonare.Models;

public static class ClipCollaboratorData
{
    public static List<ClipCollaborator> ClipCollaborators = new()
    {
        new() { ClipId = 1, UserId = 3, Role = "Mixer", Note = "Polished the final mix", CreatedAt = new DateTime(2024, 6, 4) },
        new() { ClipId = 2, UserId = 1, Role = "Drummer", Note = "Added percussive elements", CreatedAt = new DateTime(2024, 6, 3) },
        new() { ClipId = 3, UserId = 2, Role = "Producer", Note = "Helped layer ambient elements", CreatedAt = new DateTime(2024, 6, 4) }
    };
}