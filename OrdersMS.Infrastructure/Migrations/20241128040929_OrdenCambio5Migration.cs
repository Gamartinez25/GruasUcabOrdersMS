using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdersMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrdenCambio5Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdenCostoAdicional",
                table: "OrdenCostoAdicional");

            migrationBuilder.AddColumn<Guid>(
                name: "IdCostoOrden",
                table: "OrdenCostoAdicional",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "OrdenCostoAdicional",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdenCostoAdicional",
                table: "OrdenCostoAdicional",
                columns: new[] { "OrdenDeServicioId", "CostoAdicionalId", "IdCostoOrden" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdenCostoAdicional",
                table: "OrdenCostoAdicional");

            migrationBuilder.DropColumn(
                name: "IdCostoOrden",
                table: "OrdenCostoAdicional");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "OrdenCostoAdicional");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdenCostoAdicional",
                table: "OrdenCostoAdicional",
                columns: new[] { "OrdenDeServicioId", "CostoAdicionalId" });
        }
    }
}
