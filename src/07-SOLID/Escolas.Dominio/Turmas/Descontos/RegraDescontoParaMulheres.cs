using Escolas.Dominio.Alunos;
using System.Threading.Tasks;

namespace Escolas.Dominio.Turmas.Descontos
{
    public sealed class RegraDescontoParaMulheres : IRegraDesconto
    {
        public RegraDescontoParaMulheres(decimal percentualDesconto)
        {
            PercentualDesconto = percentualDesconto;
        }

        public decimal PercentualDesconto { get; }

        public Task<decimal> GerarAsync(Inscricao inscricao)
        {
            return Task.Run<decimal>(() =>
            {
                if (inscricao.Aluno.Sexo != Aluno.ESexo.Feminino)
                    return 0;
                return PercentualDesconto;
            });
        }
    }
}
