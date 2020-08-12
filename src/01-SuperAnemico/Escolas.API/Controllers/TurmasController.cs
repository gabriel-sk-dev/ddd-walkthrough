using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Escolas.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Escolas.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TurmasController : ControllerBase
    {
        private readonly IConfiguration _configuracao;

        public TurmasController(IConfiguration configuracao)
        {
            _configuracao = configuracao;
        }

        [HttpPost]
        public IActionResult Nova([FromBody] Turma novaTurma)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var sql = "INSERT INTO Turmas (Id, Descricao) VALUES (@Id, @Descricao)";
                using (var conexao = new SqlConnection(_configuracao.GetConnectionString("Escolas")))
                {
                    novaTurma.Id = Guid.NewGuid().ToString();
                    var resultado = conexao.Execute(sql, new { novaTurma.Id, novaTurma.Descricao });
                    if (resultado <= 0)
                        throw new InvalidOperationException("Não foi possível incluir a turma");
                }

                return Ok(novaTurma);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Recuperar(string id)
        {
            try
            {
                var sql = "SELECT Id, Descricao FROM Turmas WHERE Id = @id";
                using (var conexao = new SqlConnection(_configuracao.GetConnectionString("Escolas")))
                {
                    var query = conexao.Query<Turma>(sql, new { id }).ToList();
                    if (!query.Any())                    
                        return NotFound("Nenhuma turma com o id desejado");
                    return Ok(query.FirstOrDefault());
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpGet()]
        public IActionResult RecuperarTodos()
        {
            try
            {
                var sql = "SELECT Id, Descricao, LimiteAlunos FROM Turmas";
                using (var conexao = new SqlConnection(_configuracao.GetConnectionString("Escolas")))
                {
                    var query = conexao.Query<Turma>(sql, new { }).ToList();
                    if (!query.Any())
                        return new EmptyResult();
                    return Ok(query);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}
