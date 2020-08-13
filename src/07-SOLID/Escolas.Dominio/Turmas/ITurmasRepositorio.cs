using System.Threading.Tasks;

namespace Escolas.Dominio.Turmas
{
    public interface ITurmasRepositorio
    {
        Task<Turma> RecuperarAsync(string id);
        Task AdicionarAsync(Turma turma);
    }
}
