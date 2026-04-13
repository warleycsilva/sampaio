using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sampaio.Data.Migrations
{
    public partial class Add_Viagens_E_Vinculos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Viagens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Origem = table.Column<string>(type: "varchar(500)", nullable: false),
                    Destino = table.Column<string>(type: "varchar(500)", nullable: false),
                    DataPartida = table.Column<DateTime>(type: "datetime", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QtdVagas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viagens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passageiros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    ViagemId = table.Column<Guid>(nullable: false),
                    Assento = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(1000)", nullable: false),
                    Documento = table.Column<string>(type: "varchar(500)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(500)", nullable: false),
                    Comprador = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passageiros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passageiros_Viagens_ViagemId",
                        column: x => x.ViagemId,
                        principalTable: "Viagens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ViagemPagamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    ViagemId = table.Column<Guid>(nullable: false),
                    QuantidadePassagens = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdTransacao = table.Column<string>(type: "varchar(500)", nullable: true),
                    PagamentoStatus = table.Column<string>(type: "varchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViagemPagamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViagemPagamentos_Viagens_ViagemId",
                        column: x => x.ViagemId,
                        principalTable: "Viagens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Passageiros_ViagemId",
                table: "Passageiros",
                column: "ViagemId");

            migrationBuilder.CreateIndex(
                name: "IX_ViagemPagamentos_ViagemId",
                table: "ViagemPagamentos",
                column: "ViagemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passageiros");

            migrationBuilder.DropTable(
                name: "ViagemPagamentos");

            migrationBuilder.DropTable(
                name: "Viagens");
        }
    }
}
