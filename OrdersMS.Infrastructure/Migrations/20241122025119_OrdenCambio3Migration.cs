using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdersMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrdenCambio3Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poliza_Tarifa_Id",
                table: "Poliza");

            migrationBuilder.AddColumn<Guid>(
                name: "TarifaId",
                table: "Poliza",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Poliza_TarifaId",
                table: "Poliza",
                column: "TarifaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Poliza_Tarifa_TarifaId",
                table: "Poliza",
                column: "TarifaId",
                principalTable: "Tarifa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poliza_Tarifa_TarifaId",
                table: "Poliza");

            migrationBuilder.DropIndex(
                name: "IX_Poliza_TarifaId",
                table: "Poliza");

            migrationBuilder.DropColumn(
                name: "TarifaId",
                table: "Poliza");

            migrationBuilder.AddForeignKey(
                name: "FK_Poliza_Tarifa_Id",
                table: "Poliza",
                column: "Id",
                principalTable: "Tarifa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
