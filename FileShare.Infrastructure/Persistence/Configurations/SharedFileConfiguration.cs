using FileShare.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.Infrastructure.Persistence.Configurations;

public class SharedFileConfiguration : IEntityTypeConfiguration<SharedFile>
{
    public void Configure(EntityTypeBuilder<SharedFile> builder)
    {
        builder.HasKey(sf => sf.Id);

        builder.HasOne(sf => sf.Transfer)
            .WithMany(t => t.SharedFiles)
            .HasForeignKey(sf => sf.TransferId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(sf => sf.OriginalFileName).HasMaxLength(256);

        builder.Property(sf => sf.StorageKey).HasMaxLength(500);
        builder.HasIndex(sf => sf.StorageKey).IsUnique();

        builder.Property(sf => sf.ContentType).HasMaxLength(256);

        builder.Property(sf => sf.CreatedAt)
            .HasDefaultValueSql("now()");

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "ck_shared_files_original_file_name_not_empty",
                "length(btrim(original_file_name)) > 0");

            t.HasCheckConstraint(
                "ck_shared_files_file_size_bytes",
                "file_size_bytes > 0");
        });
    }
}
