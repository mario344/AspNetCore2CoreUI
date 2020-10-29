using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreUI.Web.Migrations
{
    public partial class pisosHabitaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PisoId",
                table: "Habitaciones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pisos",
                columns: table => new
                {
                    PisoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(maxLength: 100, nullable: true),
                    Estatus = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pisos", x => x.PisoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pisos");

            migrationBuilder.DropColumn(
                name: "PisoId",
                table: "Habitaciones");
        }
    }
}
