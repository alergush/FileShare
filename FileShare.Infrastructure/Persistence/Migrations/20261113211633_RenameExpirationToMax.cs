using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileShare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameExpirationToMax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "ck_subscription_tiers_default_expiration_minutes",
                table: "subscription_tiers");

            migrationBuilder.RenameColumn(
                name: "default_expiration_minutes",
                table: "subscription_tiers",
                newName: "max_expiration_minutes");

            migrationBuilder.AddCheckConstraint(
                name: "ck_subscription_tiers_max_expiration_minutes",
                table: "subscription_tiers",
                sql: "max_expiration_minutes > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "ck_subscription_tiers_max_expiration_minutes",
                table: "subscription_tiers");

            migrationBuilder.RenameColumn(
                name: "max_expiration_minutes",
                table: "subscription_tiers",
                newName: "default_expiration_minutes");

            migrationBuilder.AddCheckConstraint(
                name: "ck_subscription_tiers_default_expiration_minutes",
                table: "subscription_tiers",
                sql: "default_expiration_minutes > 0");
        }
    }
}
