using Escolas.Dominio.Alunos;
using System.Threading.Tasks;

namespace Escolas.Dominio.Turmas.Descontos
{
    public interface IRegraDesconto
    {
        decimal PercentualDesconto { get; }
        Task<decimal> GerarAsync(Inscricao inscricao);
    }
}
