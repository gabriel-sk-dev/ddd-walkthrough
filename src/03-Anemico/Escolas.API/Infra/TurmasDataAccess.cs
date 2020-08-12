using Dapper;
using Escolas.API.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Escolas.API.Infra
{
    public class TurmasDataAccess
    {
        private readonly IConfiguration _configuracao;

        public TurmasDataAccess(IConfiguration configuracao)
        {
            _configuracao = configuracao;
        }

        public Turma Recuperar(string id)
        {
            using (var conexao = new SqlConnection(_configuracao.GetConnectionString("Escolas")))
            {
                var sqlTurma = "SELECT Id, Descricao, LimiteAlunos, IdadeMinima FROM Turmas WHERE Id = @TurmaId";
                return conexao.QueryFirstOrDefault<Turma>(sqlTurma, new { id });
            }
        }
    }
}
