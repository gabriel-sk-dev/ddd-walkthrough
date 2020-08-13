using System.Threading.Tasks;

namespace Escolas.Dominio.Alunos
{
    public interface IAlunosRepositorio
    {
        Task<Aluno> RecuperarAsync(string id);
        Task AdicionarAsync(Aluno aluno);
    }
}
