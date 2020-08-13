using Microsoft.EntityFrameworkCore.Migrations;

namespace Escolas.API.Migrations
{
    public partial class configuracaodescontos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorMensal",
                table: "Turmas");

            migrationBuilder.RenameColumn(
                name: "LimiteAlunos",
                table: "Turmas",
                newName: "ConfiguracaoInscricao_LimiteAlunos");

            migrationBuilder.RenameColumn(
                name: "IdadeMinima",
                table: "Turmas",
                newName: "ConfiguracaoInscricao_IdadeMinima");

            migrationBuilder.RenameColumn(
                name: "DuracaoEmMeses",
                table: "Turmas",
                newName: "ConfiguracaoInscricao_DuracaoEmMeses");

            migrationBuilder.AlterColumn<int>(
                name: "ConfiguracaoInscricao_LimiteAlunos",
                table: "Turmas",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ConfiguracaoInscricao_IdadeMinima",
                table: "Turmas",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ConfiguracaoInscricao_DuracaoEmMeses",
                table: "Turmas",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "TipoPagamento",
                table: "Inscricoes",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "Alunos",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Bairro",
                table: "Alunos",
                type: "varchar(60)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Cep",
                table: "Alunos",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Cidade",
                table: "Alunos",
                type: "varchar(60)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Complemento",
                table: "Alunos",
                type: "varchar(40)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Endereco_DistanciaAteEscola",
                table: "Alunos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Numero",
                table: "Alunos",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Rua",
                table: "Alunos",
                type: "varchar(60)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TurmasConfiguracaoValor",
                columns: table => new
                {
                    TurmaId = table.Column<string>(nullable: false),
                    ValorMensal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescontoMaximo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmasConfiguracaoValor", x => x.TurmaId);
                    table.ForeignKey(
                        name: "FK_TurmasConfiguracaoValor_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TurmasDescontos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(37)", nullable: false),
                    RegraId = table.Column<string>(type: "varchar(100)", nullable: true),
                    PercentualDesconto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Especializacao = table.Column<string>(nullable: false),
                    TurmaId = table.Column<string>(nullable: true),
                    LimiteDistancia = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmasDescontos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TurmasDescontos_TurmasConfiguracaoValor_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "TurmasConfiguracaoValor",
                        principalColumn: "TurmaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TurmasDescontos_TurmaId",
                table: "TurmasDescontos",
                column: "TurmaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TurmasDescontos");

            migrationBuilder.DropTable(
                name: "TurmasConfiguracaoValor");

            migrationBuilder.DropColumn(
                name: "TipoPagamento",
                table: "Inscricoes");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "Endereco_Bairro",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "Endereco_Cep",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "Endereco_Cidade",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "Endereco_Complemento",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "Endereco_DistanciaAteEscola",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "Endereco_Numero",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "Endereco_Rua",
                table: "Alunos");

            migrationBuilder.RenameColumn(
                name: "ConfiguracaoInscricao_LimiteAlunos",
                table: "Turmas",
                newName: "LimiteAlunos");

            migrationBuilder.RenameColumn(
                name: "ConfiguracaoInscricao_IdadeMinima",
                table: "Turmas",
                newName: "IdadeMinima");

            migrationBuilder.RenameColumn(
                name: "ConfiguracaoInscricao_DuracaoEmMeses",
                table: "Turmas",
                newName: "DuracaoEmMeses");

            migrationBuilder.AlterColumn<int>(
                name: "LimiteAlunos",
                table: "Turmas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdadeMinima",
                table: "Turmas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DuracaoEmMeses",
                table: "Turmas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorMensal",
                table: "Turmas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
