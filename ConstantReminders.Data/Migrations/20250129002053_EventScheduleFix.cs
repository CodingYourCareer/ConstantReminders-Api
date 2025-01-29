using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstantReminders.Data.Migrations
{
    /// <inheritdoc />
    public partial class EventScheduleFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_events_notification_schedule_notification_schedule_id",
                table: "events");

            migrationBuilder.DropIndex(
                name: "ix_events_notification_schedule_id",
                table: "events");

            migrationBuilder.DropColumn(
                name: "notification_schedule_id",
                table: "events");

            migrationBuilder.AddColumn<Guid>(
                name: "event_id",
                table: "notification_schedule",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_notification_schedule_event_id",
                table: "notification_schedule",
                column: "event_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_notification_schedule_events_event_id",
                table: "notification_schedule",
                column: "event_id",
                principalTable: "events",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_notification_schedule_events_event_id",
                table: "notification_schedule");

            migrationBuilder.DropIndex(
                name: "ix_notification_schedule_event_id",
                table: "notification_schedule");

            migrationBuilder.DropColumn(
                name: "event_id",
                table: "notification_schedule");

            migrationBuilder.AddColumn<Guid>(
                name: "notification_schedule_id",
                table: "events",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_events_notification_schedule_id",
                table: "events",
                column: "notification_schedule_id");

            migrationBuilder.AddForeignKey(
                name: "fk_events_notification_schedule_notification_schedule_id",
                table: "events",
                column: "notification_schedule_id",
                principalTable: "notification_schedule",
                principalColumn: "id");
        }
    }
}
