using System;
using System.Threading.Tasks;
using Escolas.API.Models;
using Escolas.Dominio.Alunos;
using Escolas.Infra;
using Microsoft.AspNetCore.Mvc;

namespace Escolas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly EscolasContexto _escolasContexto;
        private readonly IAlunosRepositorio _alunosRepositorio;

        public AlunosController(
            EscolasContexto escolasContexto,
            IAlunosRepositorio alunosRepositorio)
        {
            _escolasContexto = escolasContexto;
            _alunosRepositorio = alunosRepositorio;
        }

        [HttpPost]
        public async Task<IActionResult> Novo([FromBody] NovoAlunoInputModel input)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var aluno = Aluno.Criar(input.Nome, input.Email, input.DataNascimento, input.Sexo.ToEnum<Aluno.ESexo>(),
                    new Endereco(input.Rua, input.Numero, input.Complemento, input.Bairro, input.Cidade, input.Cep, input.DistanciaAteEscola));
                await _alunosRepositorio.AdicionarAsync(aluno);
                await _escolasContexto.SaveChangesAsync();

                return Ok(aluno);
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
