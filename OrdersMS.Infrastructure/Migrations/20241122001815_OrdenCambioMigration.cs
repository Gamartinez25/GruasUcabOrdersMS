﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdersMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrdenCambioMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CostoTotalKm",
                table: "OrdenDeServicio",
                newName: "CostoTotalKmExtra");

            migrationBuilder.AlterColumn<Guid>(
                name: "Operador",
                table: "OrdenDeServicio",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "Administrador",
                table: "OrdenDeServicio",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "Vehiculo",
                table: "OrdenDeServicio",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vehiculo",
                table: "OrdenDeServicio");

            migrationBuilder.RenameColumn(
                name: "CostoTotalKmExtra",
                table: "OrdenDeServicio",
                newName: "CostoTotalKm");

            migrationBuilder.AlterColumn<Guid>(
                name: "Operador",
                table: "OrdenDeServicio",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Administrador",
                table: "OrdenDeServicio",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }
    }
}
