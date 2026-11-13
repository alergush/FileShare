namespace FileShare.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ICollection<UserSubscription> UserSubscriptions { get; set; } =
        new List<UserSubscription>();

    public ICollection<Transfer> Transfers { get; set; } = new List<Transfer>();
}
