using System;
using System.Threading.Tasks;
using Escolas.API.Models;
using Escolas.Dominio.Alunos;
using Escolas.Dominio.Turmas;
using Escolas.Infra;
using Microsoft.AspNetCore.Mvc;

namespace Escolas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscricoesController : ControllerBase
    {
        private readonly IAlunosRepositorio _alunosRepositorio;
        private readonly ITurmasRepositorio _turmasRepositorio;
        private readonly EscolasContexto _escolasContexto;

        public InscricoesController(
            IAlunosRepositorio alunosRepositorio,
            ITurmasRepositorio turmasRepositorio,
            EscolasContexto escolasContexto)
        {
            _alunosRepositorio = alunosRepositorio;
            _turmasRepositorio = turmasRepositorio;
            _escolasContexto = escolasContexto;
        }

        [HttpPost()]
        public async Task<IActionResult> RealizarInscricao([FromBody] InscricaoInputModel novaInscricao)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Localizar aluno                    
                var aluno = await _alunosRepositorio.RecuperarAsync(novaInscricao.AlunoId);
                if (aluno == null)
                    return BadRequest("Nenhum aluno encontrado");

                //Localizar turma                    
                var turma = await _turmasRepositorio.RecuperarAsync(novaInscricao.TurmaId);
                if (turma == null)
                    return BadRequest("Nenhuma turma encontrada");

                var inscricao = aluno.RealizarInscricao(turma, novaInscricao.TipoPagamento.ToEnum<Inscricao.ETipoPagamento>());

                //Enviar email

                //Persistir todas as alterações feitas
                await _escolasContexto.SaveChangesAsync();

                return Ok(novaInscricao);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}
