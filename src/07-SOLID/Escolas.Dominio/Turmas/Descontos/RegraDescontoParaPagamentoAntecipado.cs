using Escolas.Dominio.Alunos;
using System.Threading.Tasks;

namespace Escolas.Dominio.Turmas.Descontos
{
    public sealed class RegraDescontoParaPagamentoAntecipado : IRegraDesconto
    {
        public RegraDescontoParaPagamentoAntecipado(decimal percentualDesconto)
        {
            PercentualDesconto = percentualDesconto;
        }

        public decimal PercentualDesconto { get; }

        public Task<decimal> GerarAsync(Inscricao inscricao)
        {
            return Task.Run<decimal>(() =>
            {
                if (inscricao.TipoPagamento != Inscricao.ETipoPagamento.Antecipado)
                    return 0;
                return PercentualDesconto;
            });
        }
    }
}
