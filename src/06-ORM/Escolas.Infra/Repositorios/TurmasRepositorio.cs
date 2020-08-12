using Escolas.Dominio.Turmas;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Escolas.Infra.Repositorios
{
    public sealed class TurmasRepositorio : ITurmasRepositorio
    {
        private readonly EscolasContexto _contexto;

        public TurmasRepositorio(EscolasContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<Turma> RecuperarAsync(string id)
        {
            return await _contexto.Turmas.Where(c => c.Id == id).FirstOrDefaultAsync();
        }
    }
}
