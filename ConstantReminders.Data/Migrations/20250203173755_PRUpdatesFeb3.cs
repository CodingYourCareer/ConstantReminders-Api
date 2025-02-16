using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstantReminders.Data.Migrations
{
    /// <inheritdoc />
    public partial class PRUpdatesFeb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_days_of_week_entity_notification_schedule_notification_sche",
                table: "days_of_week_entity");

            migrationBuilder.AlterColumn<Guid>(
                name: "notification_schedule_id",
                table: "days_of_week_entity",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_days_of_week_entity_notification_schedule_notification_sche",
                table: "days_of_week_entity",
                column: "notification_schedule_id",
                principalTable: "notification_schedule",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_days_of_week_entity_notification_schedule_notification_sche",
                table: "days_of_week_entity");

            migrationBuilder.AlterColumn<Guid>(
                name: "notification_schedule_id",
                table: "days_of_week_entity",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "fk_days_of_week_entity_notification_schedule_notification_sche",
                table: "days_of_week_entity",
                column: "notification_schedule_id",
                principalTable: "notification_schedule",
                principalColumn: "id");
        }
    }
}
