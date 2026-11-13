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
                "ck_subscription_tiers_name",
                "length(btrim(name)) > 0");

            t.HasCheckConstraint(
                "ck_subscription_tiers_max_files_per_upload",
                "max_files_per_upload > 0");

            t.HasCheckConstraint(
                "ck_subscription_tiers_max_file_size_mb",
                "max_file_size_mb > 0");

            t.HasCheckConstraint(
                "ck_subscription_tiers_max_downloads",
                "max_downloads > 0");

            t.HasCheckConstraint(
                "ck_subscription_tiers_default_expiration_minutes",
                "default_expiration_minutes > 0");

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
    }
}
