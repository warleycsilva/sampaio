using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sampaio.Data.Migrations
{
    public partial class Viagem_FieldsSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Origem",
                table: "Viagens",
                type: "varchar(5000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "Destino",
                table: "Viagens",
                type: "varchar(5000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Origem",
                table: "Viagens",
                type: "varchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5000)");

            migrationBuilder.AlterColumn<string>(
                name: "Destino",
                table: "Viagens",
                type: "varchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5000)");
        }
    }
}
