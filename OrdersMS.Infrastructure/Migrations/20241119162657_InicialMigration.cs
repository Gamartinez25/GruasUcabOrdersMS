using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdersMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asegurado",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombres = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Apellidos = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    FechaNacimiento = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    TipoDocumento = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    NumeroDocumento = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Estatus = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asegurado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostoAdicional",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostoAdicional", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tarifa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CostoBase = table.Column<double>(type: "double precision", precision: 10, scale: 2, nullable: false),
                    DistanciaKm = table.Column<double>(type: "double precision", precision: 10, scale: 2, nullable: false),
                    CostoPorKm = table.Column<double>(type: "double precision", precision: 10, scale: 2, nullable: false),
                    Estatus = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Poliza",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Costo = table.Column<double>(type: "double precision", precision: 10, scale: 2, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poliza", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Poliza_Tarifa_Id",
                        column: x => x.Id,
                        principalTable: "Tarifa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PolizaAsegurado",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaInicioCobertura = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    FechaVencimientoCobertura = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Marca = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Modelo = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Anio = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    Placa = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    TipoVehiculo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Color = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Estatus = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    PolizaId = table.Column<Guid>(type: "uuid", nullable: false),
                    AseguradoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolizaAsegurado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolizaAsegurado_Asegurado_AseguradoId",
                        column: x => x.AseguradoId,
                        principalTable: "Asegurado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolizaAsegurado_Poliza_PolizaId",
                        column: x => x.PolizaId,
                        principalTable: "Poliza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdenDeServicio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DetallesIncidente = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Direccion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Estatus = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CantidadKmExtra = table.Column<double>(type: "double precision", nullable: false),
                    CostoServiciosAdicionales = table.Column<double>(type: "double precision", precision: 12, scale: 2, nullable: false),
                    CostoTotalKm = table.Column<double>(type: "double precision", precision: 12, scale: 2, nullable: false),
                    CostoTotal = table.Column<double>(type: "double precision", precision: 12, scale: 2, nullable: false),
                    NombreDenunciante = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TipoDocumentoDenunciante = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    NumeroDocumentoDenunciante = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    PolizaAseguradoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Administrador = table.Column<Guid>(type: "uuid", nullable: false),
                    Operador = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenDeServicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenDeServicio_PolizaAsegurado_PolizaAseguradoId",
                        column: x => x.PolizaAseguradoId,
                        principalTable: "PolizaAsegurado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdenCostoAdicional",
                columns: table => new
                {
                    OrdenDeServicioId = table.Column<Guid>(type: "uuid", nullable: false),
                    CostoAdicionalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Costo = table.Column<double>(type: "double precision", precision: 10, scale: 2, nullable: false),
                    Estatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenCostoAdicional", x => new { x.OrdenDeServicioId, x.CostoAdicionalId });
                    table.ForeignKey(
                        name: "FK_OrdenCostoAdicional_CostoAdicional_CostoAdicionalId",
                        column: x => x.CostoAdicionalId,
                        principalTable: "CostoAdicional",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenCostoAdicional_OrdenDeServicio_OrdenDeServicioId",
                        column: x => x.OrdenDeServicioId,
                        principalTable: "OrdenDeServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdenCostoAdicional_CostoAdicionalId",
                table: "OrdenCostoAdicional",
                column: "CostoAdicionalId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenDeServicio_PolizaAseguradoId",
                table: "OrdenDeServicio",
                column: "PolizaAseguradoId");

            migrationBuilder.CreateIndex(
                name: "IX_PolizaAsegurado_AseguradoId",
                table: "PolizaAsegurado",
                column: "AseguradoId");

            migrationBuilder.CreateIndex(
                name: "IX_PolizaAsegurado_PolizaId",
                table: "PolizaAsegurado",
                column: "PolizaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdenCostoAdicional");

            migrationBuilder.DropTable(
                name: "CostoAdicional");

            migrationBuilder.DropTable(
                name: "OrdenDeServicio");

            migrationBuilder.DropTable(
                name: "PolizaAsegurado");

            migrationBuilder.DropTable(
                name: "Asegurado");

            migrationBuilder.DropTable(
                name: "Poliza");

            migrationBuilder.DropTable(
                name: "Tarifa");
        }
    }
}
