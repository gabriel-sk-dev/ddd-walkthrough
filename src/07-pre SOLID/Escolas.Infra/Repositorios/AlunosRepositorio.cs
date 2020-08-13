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

        public async Task<Aluno> RecuperarAsync(string id)
        {
            return await _contexto
                .Alunos
                .Include(c=> c.Inscricoes)
                    .ThenInclude(c=> c.Turma)
                .Include(c=>c.Dividas)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
