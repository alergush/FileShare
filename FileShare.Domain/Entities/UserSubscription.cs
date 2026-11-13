using FileShare.Domain.Enums;

namespace FileShare.Domain.Entities;

public class UserSubscription
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SubscriptionTierId { get; set; }
    public required DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public required SubscriptionStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public User User { get; set; } = null!;
    public SubscriptionTier SubscriptionTier { get; set; } = null!;
}
