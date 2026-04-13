using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sampaio.Data.Migrations
{
    public partial class Update_Viagem_Cpf_ToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "cpf",
                table: "ViagemPagamentos",
                type: "varchar(11)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "cpf",
                table: "ViagemPagamentos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(11)");
        }
    }
}
