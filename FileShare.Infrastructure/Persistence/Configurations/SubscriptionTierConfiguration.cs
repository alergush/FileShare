using FileShare.Domain.Constants;
using FileShare.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.Infrastructure.Persistence.Configurations;

public class SubscriptionTierConfiguration : IEntityTypeConfiguration<SubscriptionTier>
{
    public void Configure(EntityTypeBuilder<SubscriptionTier> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .HasMaxLength(20);

        builder.HasIndex(t => t.Name)
            .IsUnique();

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "ck_subscription_tiers_name_not_empty",
                "length(btrim(name)) > 0");

            t.HasCheckConstraint(
                "ck_subscription_tiers_max_files_per_upload",
                "max_files_per_upload > 0");

            t.HasCheckConstraint(
                "ck_subscription_tiers_max_file_size_mb",
                "max_file_size_mb > 0");

            t.HasCheckConstraint(
                "ck_subscription_tiers_max_transfer_size_mb",
                "max_transfer_size_mb >= max_file_size_mb");

            t.HasCheckConstraint(
                "ck_subscription_tiers_max_downloads",
                "max_downloads > 0");

            t.HasCheckConstraint(
                "ck_subscription_tiers_max_expiration_minutes",
                "max_expiration_minutes > 0");

            t.HasCheckConstraint(
                "ck_subscription_tiers_max_transfers_per_window",
                "max_transfers_per_window > 0");

            t.HasCheckConstraint(
                "ck_subscription_tiers_rate_limit_window_minutes",
                "rate_limit_window_minutes > 0");

            t.HasCheckConstraint(
                "ck_subscription_tiers_price_cents",
                "price_cents >= 0");
        });

        builder.HasData(
            new SubscriptionTier
            {
                Id = 1,
                Name = SubscriptionTierNames.Guest,
                MaxFilesPerUpload = 5,
                MaxFileSizeMb = 100,
                MaxTransferSizeMb = 250,
                MaxDownloads = 5,
                MaxExpirationMinutes = 1440,
                MaxTransfersPerWindow = 3,
                RateLimitWindowMinutes = 60,
                PriceCents = 0
            },
            new SubscriptionTier
            {
                Id = 2,
                Name = SubscriptionTierNames.Free,
                MaxFilesPerUpload = 10,
                MaxFileSizeMb = 250,
                MaxTransferSizeMb = 1024,
                MaxDownloads = 20,
                MaxExpirationMinutes = 4320,
                MaxTransfersPerWindow = 10,
                RateLimitWindowMinutes = 60,
                PriceCents = 0
            },
            new SubscriptionTier
            {
                Id = 3,
                Name = SubscriptionTierNames.Pro,
                MaxFilesPerUpload = 50,
                MaxFileSizeMb = 1024,
                MaxTransferSizeMb = 10240,
                MaxDownloads = 200,
                MaxExpirationMinutes = 10080,
                MaxTransfersPerWindow = 50,
                RateLimitWindowMinutes = 60,
                PriceCents = 499
            },
            new SubscriptionTier
            {
                Id = 4,
                Name = SubscriptionTierNames.Business,
                MaxFilesPerUpload = 100,
                MaxFileSizeMb = 2048,
                MaxTransferSizeMb = 20480,
                MaxDownloads = 1000,
                MaxExpirationMinutes = 43200,
                MaxTransfersPerWindow = 200,
                RateLimitWindowMinutes = 60,
                PriceCents = 1499
            });
    }
}
