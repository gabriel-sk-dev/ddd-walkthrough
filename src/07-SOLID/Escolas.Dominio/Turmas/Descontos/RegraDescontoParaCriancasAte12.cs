using Escolas.Dominio.Alunos;
using System.Threading.Tasks;

namespace Escolas.Dominio.Turmas.Descontos
{
    public sealed class RegraDescontoParaCriancasAte12 : IRegraDesconto
    {
        public RegraDescontoParaCriancasAte12(decimal percentualDesconto)
        {
            PercentualDesconto = percentualDesconto;
        }

        public decimal PercentualDesconto { get; }

        public Task<decimal> GerarAsync(Inscricao inscricao)
        {
            return Task.Run<decimal>(() =>
            {
                if (inscricao.Aluno.IdadeHoje > 12)
                    return 0;
                return PercentualDesconto;
            });
        }
    }
}
