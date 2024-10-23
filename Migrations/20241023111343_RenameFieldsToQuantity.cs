using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp_vSem3.Migrations
{
    /// <inheritdoc />
    public partial class RenameFieldsToQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Сapacity",
                table: "Storages",
                newName: "Quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Storages",
                newName: "Сapacity");
        }
    }
}
