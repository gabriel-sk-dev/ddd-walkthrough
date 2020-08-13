using Dapper;
using Escolas.Dominio;
using Escolas.Infra.DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Linq;

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
                var sql = @"
                    SELECT Id, Descricao, LimiteAlunos, IdadeMinima, DuracaoEmMeses, ValorMensal, Aberto
                        FROM Turma
                        WHERE Id IN ( SELECT TurmaId FROM Inscricoes WHERE AlunoId = @AlunoId);
                    SELECT Id, TurmaId, AlunoId, IncritoEm FROM Inscricoes WHERE AlunoId = @AlunoId;
                    SELECT Dividas.Id, Dividas.InscricaoId, Dividas.Vencimento, Dividas.Valor, Dividas.Situacao 
                        FROM Dividas
                        INNER JOIN Inscricoes ON Inscricoes.Id = Dividas.InscricaoId
                        WHERE Inscricoes.AlunoId = @AlunoId;
                    SELECT Id, Nome, Email, DataNascimento FROM Alunos WHERE Id = @AlunoId;";
                using (var resultados = conexao.QueryMultiple(sql, new { AlunoId = id }))
                {
                    var turmas = resultados
                                    .Read<TurmaDTO>()
                                    .Select(t => new Turma(t.Id, t.Descricao, t.LimiteAlunos, t.IdadeMinima, t.TotalInscritos, t.DuracaoEmMeses, t.ValorMensal, t.Aberta));

                    var inscricoes = resultados
                                        .Read<InscricaoDTO>()
                                        .Select(i => new Inscricao(i.Id, i.AlunoId, turmas.FirstOrDefault(t => t.Id == i.TurmaId), i.InscritoEm));

                    var dividas = resultados
                                        .Read<DividaDTO>()
                                        .Select(d => new Divida(d.Id, d.InscricaoId, d.Vencimento, d.Valor, d.Situacao.ToEnum<Divida.ESituacao>()));

                    return resultados
                                    .Read<AlunoDTO>()
                                    .Select(a => new Aluno(a.Id, a.Nome, a.Email, a.DataNascimento, inscricoes, dividas ))
                                    .FirstOrDefault();
                }
            }
        }

        public void Salvar(Aluno aluno)
        {
            //Como saber o que foi alterado no aluno? 
            //Foi adicionado nova inscrição? removido inscrição? 
            //Anulado uma divida?
            //Alterado algum dado do aluno?
        }

    }
}
