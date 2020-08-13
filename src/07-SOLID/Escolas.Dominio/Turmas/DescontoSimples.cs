namespace Escolas.Dominio.Turmas
{
    public sealed class DescontoSimples : DescontoBase
    {
        public DescontoSimples(string id, string regraId, decimal percentualDesconto) : base(id, regraId, percentualDesconto) { }
    }
}
