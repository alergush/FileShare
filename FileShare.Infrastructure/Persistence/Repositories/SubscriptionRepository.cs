using FileShare.Application.Abstractions.Persistence;
using FileShare.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace FileShare.Infrastructure.Persistence.Repositories;

public sealed class SubscriptionRepository(AppDbContext context) : ISubscriptionRepository
{
    public Task<SubscriptionTier?> GetTierByNameAsync(string name, CancellationToken cancellationToken = default) =>
        context.SubscriptionTiers.FirstOrDefaultAsync(t => t.Name == name, cancellationToken);

    public Task Add(UserSubscription subscription)
    {
        context.UserSubscriptions.Add(subscription);
        return Task.CompletedTask;
    }
}
