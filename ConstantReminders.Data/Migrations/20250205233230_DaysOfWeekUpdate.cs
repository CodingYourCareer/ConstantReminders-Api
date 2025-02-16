using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstantReminders.Data.Migrations
{
    /// <inheritdoc />
    public partial class DaysOfWeekUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "day",
                table: "days_of_week_entity",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "day",
                table: "days_of_week_entity");
        }
    }
}
