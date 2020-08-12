using Dapper;
using Escolas.API.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace Escolas.API.Infra
{
    public class InscricoesDataAccess
    {
        private readonly IConfiguration _configuracao;

        public InscricoesDataAccess(IConfiguration configuracao)
        {
            _configuracao = configuracao;
        }

        public void Incluir(Inscricao inscricao)
        {
            using (var conexao = new SqlConnection(_configuracao.GetConnectionString("Escolas")))
            {
                //Incluir inscricao
                var sqlInclusao = "INSERT INTO Inscricoes (Id, AlunoId, TurmaId, InscritoEm) VALUES (@Id, @AlunoId, @TurmaId, @InscritoEm)";
                var resultado = conexao.Execute(sqlInclusao, new { inscricao.Id, inscricao.AlunoId, inscricao.TurmaId, inscricao.InscritoEm });
                if (resultado <= 0)
                    throw new InvalidOperationException("Não foi possível incluir a inscrição");
            }
        }

        public int RecuperarTotalInscritos(string turmaId)
        {
            using (var conexao = new SqlConnection(_configuracao.GetConnectionString("Escolas")))
            {
                var sqlLimiteAlunos = "SELECT COUNT(Id) FROM Inscricoes WHERE TurmaId = @TurmaId";
                return conexao.QueryFirstOrDefault<int>(sqlLimiteAlunos, new { turmaId });
            }
        }
    }
}
