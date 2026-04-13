using Microsoft.EntityFrameworkCore.Migrations;

namespace Sampaio.Data.Migrations
{
    public partial class Add_ViagemPagamento_PixQrCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PixQrCode",
                table: "ViagemPagamentos",
                type: "varchar(1000)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PixQrCode",
                table: "ViagemPagamentos");
        }
    }
}
