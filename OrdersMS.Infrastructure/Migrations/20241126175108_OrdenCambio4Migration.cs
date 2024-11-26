using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdersMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrdenCambio4Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Direccion",
                table: "OrdenDeServicio",
                newName: "DireccionOrigen");

            migrationBuilder.AddColumn<string>(
                name: "DireccionDestino",
                table: "OrdenDeServicio",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DireccionDestino",
                table: "OrdenDeServicio");

            migrationBuilder.RenameColumn(
                name: "DireccionOrigen",
                table: "OrdenDeServicio",
                newName: "Direccion");
        }
    }
}
