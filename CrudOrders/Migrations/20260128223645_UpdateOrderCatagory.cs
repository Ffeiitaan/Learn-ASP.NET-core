using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudOrders.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderCatagory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "orderType",
                table: "Orders",
                newName: "OrderCatagory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderCatagory",
                table: "Orders",
                newName: "orderType");
        }
    }
}
