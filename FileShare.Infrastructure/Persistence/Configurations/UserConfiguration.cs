using FileShare.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileShare.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FullName).HasMaxLength(50);

        builder.Property(u => u.Email).HasMaxLength(256);
        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.PasswordHash).HasMaxLength(256);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "ck_users_full_name_not_empty",
                "length(btrim(full_name)) > 0");

            t.HasCheckConstraint(
                "ck_users_email_not_empty",
                "length(btrim(email)) > 0");

            t.HasCheckConstraint(
                "ck_users_email_case",
                "email = lower(email)");
        });

        builder.HasQueryFilter(u => u.DeletedAt == null);
    }
}
