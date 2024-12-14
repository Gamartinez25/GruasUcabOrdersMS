using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdersMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CambioOrdenServicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estatus",
                table: "OrdenDeServicio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estatus",
                table: "OrdenDeServicio",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
