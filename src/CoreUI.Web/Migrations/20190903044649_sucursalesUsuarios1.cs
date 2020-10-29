using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreUI.Web.Migrations
{
    public partial class sucursalesUsuarios1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsrAlta = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UsrFechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsrFechaMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsrMod = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UsrSuperAdminMax = table.Column<byte>(type: "tinyint", nullable: false),
                    UsuarioAdministrador = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SucursalesUsuarios",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SucursalesId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SucursalesUsuarios", x => new { x.Id, x.SucursalesId });
                    table.ForeignKey(
                        name: "FK_SucursalesUsuarios_Sucursales_SucursalesId",
                        column: x => x.SucursalesId,
                        principalTable: "Sucursales",
                        principalColumn: "SucursalesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SucursalesUsuarios_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SucursalesUsuarios_SucursalesId",
                table: "SucursalesUsuarios",
                column: "SucursalesId");

            migrationBuilder.CreateIndex(
                name: "IX_SucursalesUsuarios_UsuarioId",
                table: "SucursalesUsuarios",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SucursalesUsuarios");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
