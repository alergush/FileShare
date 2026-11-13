using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FileShare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedSubscriptionTiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "subscription_tiers",
                columns: new[] { "id", "max_downloads", "max_expiration_minutes", "max_file_size_mb", "max_files_per_upload", "max_transfer_size_mb", "max_transfers_per_window", "name", "price_cents", "rate_limit_window_minutes" },
                values: new object[,]
                {
                    { 1, 5, 1440, 100, 5, 250, 3, "Guest", 0L, 60 },
                    { 2, 20, 4320, 250, 10, 1024, 10, "Free", 0L, 60 },
                    { 3, 200, 10080, 1024, 50, 10240, 50, "Pro", 499L, 60 },
                    { 4, 1000, 43200, 2048, 100, 20480, 200, "Business", 1499L, 60 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "subscription_tiers",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "subscription_tiers",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "subscription_tiers",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "subscription_tiers",
                keyColumn: "id",
                keyValue: 4);
        }
    }
}
