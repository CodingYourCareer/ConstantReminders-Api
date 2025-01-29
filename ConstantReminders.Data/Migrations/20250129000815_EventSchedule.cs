using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstantReminders.Data.Migrations
{
    /// <inheritdoc />
    public partial class EventSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "notification_schedule_id",
                table: "events",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "notification_schedule",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    notification_type = table.Column<int>(type: "integer", nullable: false),
                    created_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    frequency_within_day = table.Column<TimeSpan>(type: "interval", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    duration_in_days = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notification_schedule", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "days_of_week_entity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: false),
                    notification_schedule_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_days_of_week_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_days_of_week_entity_notification_schedule_notification_sche",
                        column: x => x.notification_schedule_id,
                        principalTable: "notification_schedule",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_events_notification_schedule_id",
                table: "events",
                column: "notification_schedule_id");

            migrationBuilder.CreateIndex(
                name: "ix_days_of_week_entity_notification_schedule_id",
                table: "days_of_week_entity",
                column: "notification_schedule_id");

            migrationBuilder.AddForeignKey(
                name: "fk_events_notification_schedule_notification_schedule_id",
                table: "events",
                column: "notification_schedule_id",
                principalTable: "notification_schedule",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_events_notification_schedule_notification_schedule_id",
                table: "events");

            migrationBuilder.DropTable(
                name: "days_of_week_entity");

            migrationBuilder.DropTable(
                name: "notification_schedule");

            migrationBuilder.DropIndex(
                name: "ix_events_notification_schedule_id",
                table: "events");

            migrationBuilder.DropColumn(
                name: "notification_schedule_id",
                table: "events");
        }
    }
}
