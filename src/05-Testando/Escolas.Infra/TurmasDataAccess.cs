using Dapper;
using Escolas.Dominio;
using Escolas.Infra.DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Linq;

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
                var sql = @"
                    SELECT Id, Descricao, LimiteAlunos, IdadeMinima, TotalInscritos, DuracaoEmMeses, ValorMensal, Aberto
                        FROM Turma
                        WHERE Id = @TurmaId;";
                return conexao
                            .Query<TurmaDTO>(sql, new { TurmaId = id })
                            .Select(t => new Turma(t.Id, t.Descricao, t.LimiteAlunos, t.IdadeMinima, t.TotalInscritos, t.DuracaoEmMeses, t.ValorMensal, t.Aberta))
                            .FirstOrDefault();
            }
        }
    }
}
