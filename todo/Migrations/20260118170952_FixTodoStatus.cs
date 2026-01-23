using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo.Migrations
{
    /// <inheritdoc />
    public partial class FixTodoStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Todos");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Todos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Todos");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Todos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
