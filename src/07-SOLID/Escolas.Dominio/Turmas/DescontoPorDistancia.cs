namespace Escolas.Dominio.Turmas
{
    public sealed class DescontoPorDistancia : DescontoBase
    {
        public DescontoPorDistancia(string id, string regraId, int limiteDistancia, decimal percentualDesconto) : base(id, regraId, percentualDesconto)
        {
            LimiteDistancia = limiteDistancia;
        }

        public int LimiteDistancia { get;  }
    }
}
