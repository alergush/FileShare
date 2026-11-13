using FileShare.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.Infrastructure.Persistence.Configurations;

public class DownloadLogConfiguration : IEntityTypeConfiguration<DownloadLog>
{
    private const int IpHashLength = 64;
    private const int UserAgentMaxLength = 500;

    public void Configure(EntityTypeBuilder<DownloadLog> builder)
    {
        builder.HasKey(dl => dl.Id);

        builder.HasOne(dl => dl.Transfer)
            .WithMany(t => t.DownloadLogs)
            .HasForeignKey(dl => dl.TransferId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(dl => dl.SharedFile)
            .WithMany()
            .HasForeignKey(dl => dl.SharedFileId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(dl => dl.IpHash).HasMaxLength(IpHashLength);

        builder.Property(dl => dl.UserAgent).HasMaxLength(UserAgentMaxLength);

        builder.Property(dl => dl.DownloadedAt)
            .HasDefaultValueSql("now()");

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "ck_download_logs_ip_hash_length",
                $"length(ip_hash) = {IpHashLength}");

            t.HasCheckConstraint(
                "ck_download_logs_ip_hash_case",
                "ip_hash = lower(ip_hash)");
        });
    }
}
