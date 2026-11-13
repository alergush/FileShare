using FileShare.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.Infrastructure.Persistence.Configurations;

public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
{
    private const int CreatorIpHashLength = 64;
    private const int CodeLength = 8;

    public void Configure(EntityTypeBuilder<Transfer> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.User)
            .WithMany(u => u.Transfers)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(t => t.CreatorIpHash).HasMaxLength(CreatorIpHashLength);

        builder.Property(t => t.Code).HasMaxLength(CodeLength);
        builder.HasIndex(t => t.Code).IsUnique();

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "ck_transfers_creator_ip_hash_length",
                $"length(creator_ip_hash) = {CreatorIpHashLength}");

            t.HasCheckConstraint(
                "ck_transfers_creator_ip_hash_case",
                "creator_ip_hash = lower(creator_ip_hash)");

            t.HasCheckConstraint(
                "ck_transfers_code_length",
                $"length(code) = {CodeLength}");

            t.HasCheckConstraint(
                "ck_transfers_code_case",
                "code = upper(code)");

            t.HasCheckConstraint(
                "ck_transfers_max_downloads",
                "max_downloads > 0");

            t.HasCheckConstraint(
                "ck_transfers_current_downloads",
                "current_downloads >= 0");

            t.HasCheckConstraint(
                "ck_transfers_within_limit",
                "current_downloads <= max_downloads");

            t.HasCheckConstraint(
                "ck_transfers_expires_at_after_created_at",
                "expires_at > created_at");
        });
    }
}
