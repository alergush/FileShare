using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FileShare.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "subscription_tiers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    max_files_per_upload = table.Column<int>(type: "integer", nullable: false),
                    max_file_size_mb = table.Column<int>(type: "integer", nullable: false),
                    max_downloads = table.Column<int>(type: "integer", nullable: false),
                    default_expiration_minutes = table.Column<int>(type: "integer", nullable: false),
                    max_transfers_per_window = table.Column<int>(type: "integer", nullable: false),
                    rate_limit_window_minutes = table.Column<int>(type: "integer", nullable: false),
                    price_cents = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subscription_tiers", x => x.id);
                    table.CheckConstraint("ck_subscription_tiers_default_expiration_minutes", "default_expiration_minutes > 0");
                    table.CheckConstraint("ck_subscription_tiers_max_downloads", "max_downloads > 0");
                    table.CheckConstraint("ck_subscription_tiers_max_file_size_mb", "max_file_size_mb > 0");
                    table.CheckConstraint("ck_subscription_tiers_max_files_per_upload", "max_files_per_upload > 0");
                    table.CheckConstraint("ck_subscription_tiers_max_transfers_per_window", "max_transfers_per_window > 0");
                    table.CheckConstraint("ck_subscription_tiers_name_not_empty", "length(btrim(name)) > 0");
                    table.CheckConstraint("ck_subscription_tiers_price_cents", "price_cents >= 0");
                    table.CheckConstraint("ck_subscription_tiers_rate_limit_window_minutes", "rate_limit_window_minutes > 0");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.CheckConstraint("ck_users_email_case", "email = lower(email)");
                    table.CheckConstraint("ck_users_email_not_empty", "length(btrim(email)) > 0");
                    table.CheckConstraint("ck_users_full_name_not_empty", "length(btrim(full_name)) > 0");
                });

            migrationBuilder.CreateTable(
                name: "transfers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    creator_ip_hash = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    max_downloads = table.Column<int>(type: "integer", nullable: false),
                    current_downloads = table.Column<int>(type: "integer", nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transfers", x => x.id);
                    table.CheckConstraint("ck_transfers_code_case", "code = upper(code)");
                    table.CheckConstraint("ck_transfers_code_length", "length(code) = 8");
                    table.CheckConstraint("ck_transfers_creator_ip_hash_case", "creator_ip_hash = lower(creator_ip_hash)");
                    table.CheckConstraint("ck_transfers_creator_ip_hash_length", "length(creator_ip_hash) = 64");
                    table.CheckConstraint("ck_transfers_current_downloads", "current_downloads >= 0");
                    table.CheckConstraint("ck_transfers_expires_at_after_created_at", "expires_at > created_at");
                    table.CheckConstraint("ck_transfers_max_downloads", "max_downloads > 0");
                    table.CheckConstraint("ck_transfers_within_limit", "current_downloads <= max_downloads");
                    table.ForeignKey(
                        name: "fk_transfers_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_subscriptions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    subscription_tier_id = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_subscriptions", x => x.id);
                    table.CheckConstraint("ck_user_subscriptions_end_after_start", "end_date IS NULL OR end_date > start_date");
                    table.ForeignKey(
                        name: "fk_user_subscriptions_subscription_tiers_subscription_tier_id",
                        column: x => x.subscription_tier_id,
                        principalTable: "subscription_tiers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user_subscriptions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "shared_files",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transfer_id = table.Column<int>(type: "integer", nullable: false),
                    original_file_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    storage_key = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    file_size_bytes = table.Column<long>(type: "bigint", nullable: false),
                    content_type = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shared_files", x => x.id);
                    table.CheckConstraint("ck_shared_files_file_size_bytes", "file_size_bytes > 0");
                    table.CheckConstraint("ck_shared_files_original_file_name_not_empty", "length(btrim(original_file_name)) > 0");
                    table.ForeignKey(
                        name: "fk_shared_files_transfers_transfer_id",
                        column: x => x.transfer_id,
                        principalTable: "transfers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "download_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transfer_id = table.Column<int>(type: "integer", nullable: false),
                    shared_file_id = table.Column<int>(type: "integer", nullable: true),
                    downloaded_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    ip_hash = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    user_agent = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_download_logs", x => x.id);
                    table.CheckConstraint("ck_download_logs_ip_hash_case", "ip_hash = lower(ip_hash)");
                    table.CheckConstraint("ck_download_logs_ip_hash_length", "length(ip_hash) = 64");
                    table.ForeignKey(
                        name: "fk_download_logs_shared_files_shared_file_id",
                        column: x => x.shared_file_id,
                        principalTable: "shared_files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_download_logs_transfers_transfer_id",
                        column: x => x.transfer_id,
                        principalTable: "transfers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_download_logs_shared_file_id",
                table: "download_logs",
                column: "shared_file_id");

            migrationBuilder.CreateIndex(
                name: "ix_download_logs_transfer_id",
                table: "download_logs",
                column: "transfer_id");

            migrationBuilder.CreateIndex(
                name: "ix_shared_files_storage_key",
                table: "shared_files",
                column: "storage_key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_shared_files_transfer_id",
                table: "shared_files",
                column: "transfer_id");

            migrationBuilder.CreateIndex(
                name: "ix_subscription_tiers_name",
                table: "subscription_tiers",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_transfers_code",
                table: "transfers",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_transfers_user_id",
                table: "transfers",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_subscriptions_one_active_per_user",
                table: "user_subscriptions",
                column: "user_id",
                unique: true,
                filter: "status = 'Active'");

            migrationBuilder.CreateIndex(
                name: "ix_user_subscriptions_subscription_tier_id",
                table: "user_subscriptions",
                column: "subscription_tier_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "download_logs");

            migrationBuilder.DropTable(
                name: "user_subscriptions");

            migrationBuilder.DropTable(
                name: "shared_files");

            migrationBuilder.DropTable(
                name: "subscription_tiers");

            migrationBuilder.DropTable(
                name: "transfers");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
