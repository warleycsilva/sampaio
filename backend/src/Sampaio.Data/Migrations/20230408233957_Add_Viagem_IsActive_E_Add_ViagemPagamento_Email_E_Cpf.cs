using Microsoft.EntityFrameworkCore.Migrations;

namespace Sampaio.Data.Migrations
{
    public partial class Add_Viagem_IsActive_E_Add_ViagemPagamento_Email_E_Cpf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "Viagens",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "cpf",
                table: "ViagemPagamentos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "ViagemPagamentos",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "Viagens");

            migrationBuilder.DropColumn(
                name: "cpf",
                table: "ViagemPagamentos");

            migrationBuilder.DropColumn(
                name: "email",
                table: "ViagemPagamentos");
        }
    }
}
