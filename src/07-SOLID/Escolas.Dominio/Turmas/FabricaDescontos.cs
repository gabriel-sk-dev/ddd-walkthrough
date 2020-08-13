using Escolas.Dominio.Turmas.Descontos;
using System;

namespace Escolas.Dominio.Turmas
{
    public static class FabricaDescontos
    {
        public static IRegraDesconto Criar(DescontoBase desconto) 
            => desconto.RegraId switch 
            { 
                (nameof(RegraDescontoParaMulheres)) => new RegraDescontoParaMulheres(desconto.PercentualDesconto),
                (nameof(RegraDescontoParaCriancasAte12)) => new RegraDescontoParaCriancasAte12(desconto.PercentualDesconto),
                (nameof(RegraDescontoParaPagamentoAntecipado)) => new RegraDescontoParaPagamentoAntecipado(desconto.PercentualDesconto),
                (nameof(RegraDescontoPorDistancia)) => new RegraDescontoPorDistancia(desconto.PercentualDesconto, ((DescontoPorDistancia)desconto).LimiteDistancia),
                _ => throw new InvalidOperationException("Regra informada não é válida")
            };
    }
}
