using FileShare.Domain.Entities;

namespace FileShare.Application.Abstractions.Persistence;

public interface ISubscriptionRepository
{
    Task<SubscriptionTier?> GetTierByNameAsync(string name, CancellationToken cancellationToken = default);
    Task Add(UserSubscription subscription);
}
