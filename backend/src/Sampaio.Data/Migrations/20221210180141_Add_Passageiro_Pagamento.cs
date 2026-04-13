using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sampaio.Data.Migrations
{
    public partial class Add_Passageiro_Pagamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ViagemPagamentoId",
                table: "Passageiros",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passageiros_ViagemPagamentoId",
                table: "Passageiros",
                column: "ViagemPagamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passageiros_ViagemPagamentos_ViagemPagamentoId",
                table: "Passageiros",
                column: "ViagemPagamentoId",
                principalTable: "ViagemPagamentos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passageiros_ViagemPagamentos_ViagemPagamentoId",
                table: "Passageiros");

            migrationBuilder.DropIndex(
                name: "IX_Passageiros_ViagemPagamentoId",
                table: "Passageiros");

            migrationBuilder.DropColumn(
                name: "ViagemPagamentoId",
                table: "Passageiros");
        }
    }
}
