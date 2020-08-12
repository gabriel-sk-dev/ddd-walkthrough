using Dapper;
using Escolas.API.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Escolas.API.Infra
{
    public class AlunosDataAccess
    {
        private readonly IConfiguration _configuracao;

        public AlunosDataAccess(IConfiguration configuracao)
        {
            _configuracao = configuracao;
        }

        public Aluno Recuperar(string id)
        {
            using (var conexao = new SqlConnection(_configuracao.GetConnectionString("Escolas")))
            {
                var sqlAlunos = "SELECT Id, Nome, Email, DataNascimento FROM Alunos WHERE Id = @AlunoId";
                return conexao.QueryFirstOrDefault<Aluno>(sqlAlunos, new { id });
            }
        }
    }
}
