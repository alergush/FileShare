using FileShare.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.Infrastructure.Persistence.Configurations;

public class UserSubscriptionConfiguration : IEntityTypeConfiguration<UserSubscription>
{
    public void Configure(EntityTypeBuilder<UserSubscription> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.User)
            .WithMany(u => u.UserSubscriptions)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.SubscriptionTier)
            .WithMany()
            .HasForeignKey(s => s.SubscriptionTierId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(s => s.UserId, "ix_user_subscriptions_one_active_per_user")
            .IsUnique()
            .HasFilter("status = 'Active'");

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "ck_user_subscriptions_end_after_start",
                "end_date IS NULL OR end_date > start_date");
        });

        builder.HasQueryFilter(s => s.User.DeletedAt == null);
    }
}
