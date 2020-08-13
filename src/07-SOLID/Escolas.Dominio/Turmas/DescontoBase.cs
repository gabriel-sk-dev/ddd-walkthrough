using Escolas.Dominio.Alunos;
using Escolas.Dominio.Turmas.Descontos;
using System.Threading.Tasks;

namespace Escolas.Dominio.Turmas
{
    public abstract class DescontoBase
    {
        protected DescontoBase(string id, string regraId, decimal percentualDesconto)
        {
            Id = id;
            RegraId = regraId;
            PercentualDesconto = percentualDesconto;
        }

        public string Id { get; }
        public string RegraId { get; }
        public decimal PercentualDesconto { get; }
        public IRegraDesconto Regra => FabricaDescontos.Criar(this);

        public Task<decimal> Gerar(Inscricao inscricao)
        {
            return Regra.GerarAsync(inscricao);
        }
    }
}
