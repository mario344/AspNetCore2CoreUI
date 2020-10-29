using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreUI.Web.Migrations
{
    public partial class creaegresos1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoEgresos",
                columns: table => new
                {
                    TipoEgresosId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SucursalesId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 100, nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    UsrAlta = table.Column<string>(maxLength: 60, nullable: true),
                    UsrFechaAlta = table.Column<DateTime>(nullable: false),
                    UsrMod = table.Column<string>(maxLength: 60, nullable: true),
                    UsrFechaMod = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEgresos", x => x.TipoEgresosId);
                });

            migrationBuilder.CreateTable(
                name: "Egresos",
                columns: table => new
                {
                    EgresosId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SucursalesId = table.Column<int>(nullable: false),
                    TipoEgresosId = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(maxLength: 100, nullable: true),
                    Cantidad = table.Column<decimal>(nullable: false),
                    Ajuste = table.Column<byte>(nullable: false),
                    Cancelado = table.Column<byte>(nullable: false),
                    MotivoAjusteCancelacion = table.Column<string>(maxLength: 50, nullable: true),
                    UsrAlta = table.Column<string>(maxLength: 60, nullable: true),
                    UsrFechaAlta = table.Column<DateTime>(nullable: false),
                    UsrMod = table.Column<string>(maxLength: 60, nullable: true),
                    UsrFechaMod = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Egresos", x => x.EgresosId);
                    table.ForeignKey(
                        name: "FK_Egresos_TipoEgresos_TipoEgresosId",
                        column: x => x.TipoEgresosId,
                        principalTable: "TipoEgresos",
                        principalColumn: "TipoEgresosId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Egresos_TipoEgresosId",
                table: "Egresos",
                column: "TipoEgresosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Egresos");

            migrationBuilder.DropTable(
                name: "TipoEgresos");
        }
    }
}
