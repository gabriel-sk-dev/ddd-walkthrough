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
        private readonly InscricoesDataAccess _incricoesDataAcess;

        public InscricoesController(
            AlunosDataAccess alunosDataAccess,
            TurmasDataAccess turmasDataAcess,
            InscricoesDataAccess incricoesDataAcess)
        {
            _alunosDataAccess = alunosDataAccess;
            _turmasDataAcess = turmasDataAcess;
            _incricoesDataAcess = incricoesDataAcess;
        }

        [HttpPost()]
        public IActionResult RealizarInscricao([FromBody] Inscricao novaInscricao)
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

                //Regra idade
                if (aluno.DataNascimento.CalcularIdade() < turma.IdadeMinima)
                    return BadRequest("Aluno não possui idade suficiente para se inscrever na turma");

                //Limite de alunos
                var totalInscritos = _incricoesDataAcess.RecuperarTotalInscritos(novaInscricao.TurmaId);
                if (totalInscritos > turma.LimiteAlunos)
                    return BadRequest("Limite de inscritos da turma foi atingido");

                //Turma deve estar aberta
                if (!turma.Aberta)
                    return BadRequest("Turma não está aberta para inscrições");

                //Atribuir valores
                novaInscricao.Id = Guid.NewGuid().ToString();
                novaInscricao.InscritoEm = DateTime.Now;

                #region Implementação de novas regras
                /* 
                 * Aluno não pode ter débitos 
                 * Gerar mensalidades conforme contrato
                 * Enviar Email
                 */
                #endregion

                //Incluir inscricao
                _incricoesDataAcess.Incluir(novaInscricao);

                return Ok(novaInscricao);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}
