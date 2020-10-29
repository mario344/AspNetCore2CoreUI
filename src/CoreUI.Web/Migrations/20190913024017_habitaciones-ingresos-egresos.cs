using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreUI.Web.Migrations
{
    public partial class habitacionesingresosegresos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UsrMod",
                table: "Sucursales",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UsrAlta",
                table: "Sucursales",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TiposHabitacion",
                columns: table => new
                {
                    TiposHabitacionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<bool>(type: "bit", nullable: false),
                    NoPesonas = table.Column<byte>(type: "tinyint", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NombreCorto = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    PrecioHabitacion = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    PrecioNinioExtra = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    PrecioPersonaExtra = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    SucursalesId = table.Column<int>(type: "int", nullable: false),
                    UsrAlta = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UsrFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsrFechaMod = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrMod = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposHabitacion", x => x.TiposHabitacionID);
                    table.ForeignKey(
                        name: "FK_TiposHabitacion_Sucursales_SucursalesId",
                        column: x => x.SucursalesId,
                        principalTable: "Sucursales",
                        principalColumn: "SucursalesId");
                });

            migrationBuilder.CreateTable(
                name: "TiposIngresos",
                columns: table => new
                {
                    TiposIngresosId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<bool>(type: "bit", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SucursalesId = table.Column<int>(type: "int", nullable: false),
                    UsrAlta = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UsrFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsrFechaMod = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrMod = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposIngresos", x => x.TiposIngresosId);
                    table.ForeignKey(
                        name: "FK_TiposIngresos_Sucursales_SucursalesId",
                        column: x => x.SucursalesId,
                        principalTable: "Sucursales",
                        principalColumn: "SucursalesId");
                });

            migrationBuilder.CreateTable(
                name: "Zonas",
                columns: table => new
                {
                    ZonasId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estatus = table.Column<bool>(type: "bit", nullable: false),
                    SucursalesId = table.Column<int>(type: "int", nullable: false),
                    UsrAlta = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UsrFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsrFechaMod = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrMod = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zonas", x => x.ZonasId);
                    table.ForeignKey(
                        name: "FK_Zonas_Sucursales_SucursalesId",
                        column: x => x.SucursalesId,
                        principalTable: "Sucursales",
                        principalColumn: "SucursalesId");
                });

            migrationBuilder.CreateTable(
                name: "Habitaciones",
                columns: table => new
                {
                    HabitacionesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaveHabitacion = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Estatus = table.Column<bool>(type: "bit", nullable: false),
                    EstatusAdminitrador = table.Column<byte>(type: "tinyint", nullable: false),
                    NoHabitacion = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NombreCorto = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SucursalesId = table.Column<int>(type: "int", nullable: false),
                    TiposHabitacionID = table.Column<int>(type: "int", nullable: false),
                    UsrAlta = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UsrFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsrFechaMod = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrMod = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitaciones", x => x.HabitacionesId);
                    table.ForeignKey(
                        name: "FK_Habitaciones_Sucursales_SucursalesId",
                        column: x => x.SucursalesId,
                        principalTable: "Sucursales",
                        principalColumn: "SucursalesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Habitaciones_TiposHabitacion_TiposHabitacionID",
                        column: x => x.TiposHabitacionID,
                        principalTable: "TiposHabitacion",
                        principalColumn: "TiposHabitacionID");
                });

            migrationBuilder.CreateTable(
                name: "Ingresos",
                columns: table => new
                {
                    IngresosId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ajuste = table.Column<byte>(type: "tinyint", nullable: true),
                    Cancelado = table.Column<byte>(type: "tinyint", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MotivoAjusteCancelacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SucursalesId = table.Column<int>(type: "int", nullable: false),
                    TiposIngresosId = table.Column<int>(type: "int", nullable: false),
                    UsrAlta = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UsrFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsrFechaMod = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrMod = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingresos", x => x.IngresosId);
                    table.ForeignKey(
                        name: "FK_Ingresos_Sucursales_SucursalesId",
                        column: x => x.SucursalesId,
                        principalTable: "Sucursales",
                        principalColumn: "SucursalesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingresos_TiposIngresos_TiposIngresosId",
                        column: x => x.TiposIngresosId,
                        principalTable: "TiposIngresos",
                        principalColumn: "TiposIngresosId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Habitaciones_SucursalesId",
                table: "Habitaciones",
                column: "SucursalesId");

            migrationBuilder.CreateIndex(
                name: "IX_Habitaciones_TiposHabitacionID",
                table: "Habitaciones",
                column: "TiposHabitacionID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_SucursalesId",
                table: "Ingresos",
                column: "SucursalesId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_TiposIngresosId",
                table: "Ingresos",
                column: "TiposIngresosId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposHabitacion_SucursalesId",
                table: "TiposHabitacion",
                column: "SucursalesId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposIngresos_SucursalesId",
                table: "TiposIngresos",
                column: "SucursalesId");

            migrationBuilder.CreateIndex(
                name: "IX_Zonas_SucursalesId",
                table: "Zonas",
                column: "SucursalesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Habitaciones");

            migrationBuilder.DropTable(
                name: "Ingresos");

            migrationBuilder.DropTable(
                name: "Zonas");

            migrationBuilder.DropTable(
                name: "TiposHabitacion");

            migrationBuilder.DropTable(
                name: "TiposIngresos");

            migrationBuilder.AlterColumn<string>(
                name: "UsrMod",
                table: "Sucursales",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UsrAlta",
                table: "Sucursales",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true);
        }
    }
}
