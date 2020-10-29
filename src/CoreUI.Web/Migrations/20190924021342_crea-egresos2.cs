using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreUI.Web.Migrations
{
    public partial class creaegresos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TipoEgresos_SucursalesId",
                table: "TipoEgresos",
                column: "SucursalesId");

            migrationBuilder.CreateIndex(
                name: "IX_Egresos_SucursalesId",
                table: "Egresos",
                column: "SucursalesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Egresos_Sucursales_SucursalesId",
                table: "Egresos",
                column: "SucursalesId",
                principalTable: "Sucursales",
                principalColumn: "SucursalesId");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoEgresos_Sucursales_SucursalesId",
                table: "TipoEgresos",
                column: "SucursalesId",
                principalTable: "Sucursales",
                principalColumn: "SucursalesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Egresos_Sucursales_SucursalesId",
                table: "Egresos");

            migrationBuilder.DropForeignKey(
                name: "FK_TipoEgresos_Sucursales_SucursalesId",
                table: "TipoEgresos");

            migrationBuilder.DropIndex(
                name: "IX_TipoEgresos_SucursalesId",
                table: "TipoEgresos");

            migrationBuilder.DropIndex(
                name: "IX_Egresos_SucursalesId",
                table: "Egresos");
        }
    }
}
