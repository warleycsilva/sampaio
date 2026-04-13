using Microsoft.EntityFrameworkCore.Migrations;

namespace Sampaio.Data.Migrations
{
    public partial class Add_Viagem_Observacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacoes",
                table: "Viagens",
                type: "varchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacoes",
                table: "Viagens");
        }
    }
}
