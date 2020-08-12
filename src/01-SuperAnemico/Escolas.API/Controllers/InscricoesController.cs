using System;
using System.Data.SqlClient;
using Dapper;
using Escolas.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Escolas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscricoesController : ControllerBase
    {
        private readonly IConfiguration _configuracao;

        public InscricoesController(IConfiguration configuracao)
        {
            _configuracao = configuracao;
        }

        [HttpPost()]
        public IActionResult RealizarInscricao([FromBody] Inscricao novaInscricao)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                using (var conexao = new SqlConnection(_configuracao.GetConnectionString("Escolas")))
                {
                    //Localizar aluno
                    var sqlAlunos = "SELECT Id, Nome, Email, DataNascimento FROM Alunos WHERE Id = @AlunoId";
                    var aluno = conexao.QueryFirstOrDefault<Aluno>(sqlAlunos, new { novaInscricao.AlunoId });
                    if (aluno == null)
                        return BadRequest("Nenhum aluno encontrado");

                    //Localizar turma
                    var sqlTurma = "SELECT Id, Descricao FROM Turmas WHERE Id = @TurmaId";
                    var turma = conexao.QueryFirstOrDefault<Turma>(sqlTurma, new { novaInscricao.TurmaId });
                    if (turma == null)
                        return BadRequest("Nenhuma turma encontrada");

                    novaInscricao.Id = Guid.NewGuid().ToString();
                    novaInscricao.InscritoEm = DateTime.Now;

                    //Incluir inscricao
                    var sqlInclusao = "INSERT INTO Inscricoes (Id, AlunoId, TurmaId, InscritoEm) VALUES (@Id, @AlunoId, @TurmaId, @InscritoEm)";
                    var resultado = conexao.Execute(sqlInclusao, new { novaInscricao.Id, novaInscricao.AlunoId, novaInscricao.TurmaId, novaInscricao.InscritoEm });
                    if (resultado <= 0)
                        throw new InvalidOperationException("Não foi possível incluir a inscrição");

                    return Ok(novaInscricao);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}
