using Sonare.Models;

public static class UserData
{
    public static List<User> Users = new()
    {
        new() { Id = 1, Username = "melodyMaker", Email = "melody@example.com", PasswordHash = "hash1", CreatedAt = new DateTime(2024, 5, 16) },
        new() { Id = 2, Username = "beatSmith", Email = "beats@example.com", PasswordHash = "hash2", CreatedAt = new DateTime(2024, 5, 21) },
        new() { Id = 3, Username = "synthQueen", Email = "synth@example.com", PasswordHash = "hash3", CreatedAt = new DateTime(2024, 5, 26) }
    };
}
