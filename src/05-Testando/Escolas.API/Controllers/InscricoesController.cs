using System;
using Escolas.API.Infra;
using Escolas.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Escolas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscricoesController : ControllerBase
    {
        private readonly AlunosDataAccess _alunosDataAccess;
        private readonly TurmasDataAccess _turmasDataAcess;

        public InscricoesController(
            AlunosDataAccess alunosDataAccess,
            TurmasDataAccess turmasDataAcess)
        {
            _alunosDataAccess = alunosDataAccess;
            _turmasDataAcess = turmasDataAcess;
        }

        [HttpPost()]
        public IActionResult RealizarInscricao([FromBody] InscricaoInputModel novaInscricao)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //Localizar aluno                    
                var aluno = _alunosDataAccess.Recuperar(novaInscricao.AlunoId);
                if (aluno == null)
                    return BadRequest("Nenhum aluno encontrado");

                //Localizar turma                    
                var turma = _turmasDataAcess.Recuperar(novaInscricao.TurmaId);
                if (turma == null)
                    return BadRequest("Nenhuma turma encontrada");

                var inscricao = aluno.RealizarInscricao(turma);

                //Enviar email

                //Salvar inscrição
                _alunosDataAccess.Salvar(aluno);

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
