using FileShare.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace FileShare.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<SubscriptionTier> SubscriptionTiers => Set<SubscriptionTier>();
    public DbSet<UserSubscription> UserSubscriptions => Set<UserSubscription>();
    public DbSet<Transfer> Transfers => Set<Transfer>();
    public DbSet<SharedFile> SharedFiles => Set<SharedFile>();
    public DbSet<DownloadLog> DownloadLogs => Set<DownloadLog>();
}
