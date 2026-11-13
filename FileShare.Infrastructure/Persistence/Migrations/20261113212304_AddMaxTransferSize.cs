using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileShare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMaxTransferSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "max_transfer_size_mb",
                table: "subscription_tiers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddCheckConstraint(
                name: "ck_subscription_tiers_max_transfer_size_mb",
                table: "subscription_tiers",
                sql: "max_transfer_size_mb >= max_file_size_mb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "ck_subscription_tiers_max_transfer_size_mb",
                table: "subscription_tiers");

            migrationBuilder.DropColumn(
                name: "max_transfer_size_mb",
                table: "subscription_tiers");
        }
    }
}
