using Escolas.Dominio.Alunos;
using System.Threading.Tasks;

namespace Escolas.Dominio.Turmas.Descontos
{
    public sealed class RegraDescontoPorDistancia : IRegraDesconto
    {
        public RegraDescontoPorDistancia(decimal percentualDesconto, int limiteDistancia)
        {
            PercentualDesconto = percentualDesconto;
            LimiteDistancia = limiteDistancia;
        }

        public decimal PercentualDesconto { get; }
        public int LimiteDistancia { get; }

        public Task<decimal> GerarAsync(Inscricao inscricao)
        {
            return Task.Run<decimal>(() =>
            {
                if (inscricao.Aluno.Endereco.DistanciaAteEscola > LimiteDistancia)
                    return 0;
                return PercentualDesconto;
            });
        }
    }
}
