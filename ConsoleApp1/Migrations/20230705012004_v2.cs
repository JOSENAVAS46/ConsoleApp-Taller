using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp1.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idCliente",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "idMascota",
                table: "CitasMedicas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idCliente",
                table: "Mascotas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idMascota",
                table: "CitasMedicas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
