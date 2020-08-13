using System;
using System.Threading.Tasks;
using Escolas.API.Models;
using Escolas.Dominio.Turmas;
using Escolas.Dominio.Turmas.Descontos;
using Escolas.Infra;
using Microsoft.AspNetCore.Mvc;

namespace Escolas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmasController : ControllerBase
    {
        private readonly EscolasContexto _escolasContexto;
        private readonly ITurmasRepositorio _turmasRepositorio;

        public TurmasController(
            EscolasContexto escolasContexto,
            ITurmasRepositorio turmasRepositorio)
        {
            _escolasContexto = escolasContexto;
            _turmasRepositorio = turmasRepositorio;
        }

        [HttpPost]
        public async Task<IActionResult> Novo([FromBody] NovaTurmaInputModel input)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var turma = Turma.CriarFechada(input.Descricao, input.LimiteAlunos, input.IdadeMinima, input.Duracao, input.ValorMensal);
                await _turmasRepositorio.AdicionarAsync(turma);
                await _escolasContexto.SaveChangesAsync();

                return Ok(turma);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPut("{id}/DescontoMaximo")]
        public async Task<IActionResult> ConfigurarDescontoMaximo(
            [FromBody] DescontoMaximoInputModel input,
            string id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var turma = await _turmasRepositorio.RecuperarAsync(id);
                if (turma == null)
                    return NotFound();
                turma.ConfigurarDescontoMaximo(input.Valor);
                await _escolasContexto.SaveChangesAsync();

                return Ok(turma);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPut("{id}/AplicarDesconto")]
        public async Task<IActionResult> AplicarDesconto(
            [FromBody] DescontoInputModel input,
            string id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var turma = await _turmasRepositorio.RecuperarAsync(id);
                if (turma == null)
                    return NotFound();
                var desconto = input.Regra switch
                {
                    ("RegraDescontoParaCriancasAte12") => new RegraDescontoParaCriancasAte12(input.Valor) as IRegraDesconto,
                    ("RegraDescontoParaMulheres") => new RegraDescontoParaMulheres(input.Valor) as IRegraDesconto,
                    ("RegraDescontoParaPagamentoAntecipado") => new RegraDescontoParaPagamentoAntecipado(input.Valor) as IRegraDesconto,
                    ("RegraDescontoPorDistancia") => new RegraDescontoPorDistancia(input.Valor, input.LimiteDistancia) as IRegraDesconto,
                    _ => throw new ArgumentException("Regra informada é inválida", nameof(input.Regra))
                };
                turma.AplicarDesconto(desconto);
                await _escolasContexto.SaveChangesAsync();

                return Ok(turma);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        [HttpPut("{id}/Abrir")]
        public async Task<IActionResult> Abrir(string id)
        {
            try
            {
                var turma = await _turmasRepositorio.RecuperarAsync(id);
                if (turma == null)
                    return NotFound();
                turma.AbrirParaInscricoes();
                await _escolasContexto.SaveChangesAsync();

                return Ok(turma);
            }
            catch (InvalidOperationException ex)
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
