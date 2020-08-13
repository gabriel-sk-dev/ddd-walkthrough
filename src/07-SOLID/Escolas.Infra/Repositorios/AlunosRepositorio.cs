using Escolas.Dominio.Alunos;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Escolas.Infra.Repositorios
{
    public sealed class AlunosRepositorio : IAlunosRepositorio
    {
        private readonly EscolasContexto _contexto;

        public AlunosRepositorio(EscolasContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task AdicionarAsync(Aluno aluno)
        {
            await _contexto.Alunos.AddAsync(aluno);
        }

        public async Task<Aluno> RecuperarAsync(string id)
        {
            return await _contexto
                .Alunos
                .Include(c=> c.Inscricoes)
                    .ThenInclude(c=> c.Turma)
                        .ThenInclude(c=> c.ConfiguracaoValor)
                            .ThenInclude(c => c.Descontos)
                .Include(c=>c.Dividas)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
