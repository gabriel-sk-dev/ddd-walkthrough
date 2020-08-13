using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Escolas.API.Migrations
{
    public partial class veersao_inicial_inscricoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(37)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(37)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: true),
                    LimiteAlunos = table.Column<int>(nullable: false),
                    IdadeMinima = table.Column<int>(nullable: false),
                    TotalInscritos = table.Column<int>(nullable: false),
                    DuracaoEmMeses = table.Column<int>(nullable: false),
                    ValorMensal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Aberta = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inscricoes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(37)", nullable: false),
                    AlunoId = table.Column<string>(nullable: true),
                    TurmaId = table.Column<string>(nullable: true),
                    InscritoEm = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscricoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inscricoes_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inscricoes_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dividas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(37)", nullable: false),
                    AlunoId = table.Column<string>(nullable: true),
                    InscricaoId = table.Column<string>(nullable: true),
                    Vencimento = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Situacao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dividas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dividas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dividas_Inscricoes_InscricaoId",
                        column: x => x.InscricaoId,
                        principalTable: "Inscricoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dividas_AlunoId",
                table: "Dividas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Dividas_InscricaoId",
                table: "Dividas",
                column: "InscricaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscricoes_AlunoId",
                table: "Inscricoes",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscricoes_TurmaId",
                table: "Inscricoes",
                column: "TurmaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dividas");

            migrationBuilder.DropTable(
                name: "Inscricoes");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Turmas");
        }
    }
}
