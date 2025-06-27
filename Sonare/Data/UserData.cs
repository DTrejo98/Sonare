using Sonare.Models;

public static class UserData
{
    public static List<User> Users = new()
    {
        new() { Id = 1, Uid = "googleUid1", Username = "melodyMaker", Email = "melody@example.com", Password = "hash1", CreatedAt = new DateTime(2024, 5, 16) },
        new() { Id = 2, Uid = "googleUid2", Username = "beatSmith", Email = "beats@example.com", Password = "hash2", CreatedAt = new DateTime(2024, 5, 21) },
        new() { Id = 3, Uid = "googleUid3", Username = "synthQueen", Email = "synth@example.com", Password = "hash3", CreatedAt = new DateTime(2024, 5, 26) }
    };
}
