using Microsoft.EntityFrameworkCore.Migrations;

namespace Escolas.API.Migrations
{
    public partial class versao_com_calculo_desconto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorMensal",
                table: "Turmas",
                newName: "ConfiguracaoValor_ValorMensal");

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

            migrationBuilder.AlterColumn<decimal>(
                name: "ConfiguracaoValor_ValorMensal",
                table: "Turmas",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

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

            migrationBuilder.AddColumn<decimal>(
                name: "ConfiguracaoValor_DescontoCriancas",
                table: "Turmas",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ConfiguracaoValor_DescontoDistancia",
                table: "Turmas",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ConfiguracaoValor_DescontoMaximo",
                table: "Turmas",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ConfiguracaoValor_DescontoMulheres",
                table: "Turmas",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ConfiguracaoValor_DescontoPagamentoAntecipado",
                table: "Turmas",
                type: "decimal(18,2)",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfiguracaoValor_DescontoCriancas",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "ConfiguracaoValor_DescontoDistancia",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "ConfiguracaoValor_DescontoMaximo",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "ConfiguracaoValor_DescontoMulheres",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "ConfiguracaoValor_DescontoPagamentoAntecipado",
                table: "Turmas");

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
                name: "ConfiguracaoValor_ValorMensal",
                table: "Turmas",
                newName: "ValorMensal");

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

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorMensal",
                table: "Turmas",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

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
        }
    }
}
