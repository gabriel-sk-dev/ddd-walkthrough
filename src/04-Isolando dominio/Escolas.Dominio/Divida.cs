using System;

namespace Escolas.Dominio
{
    public sealed class Divida
    {
        public Divida(string id, string inscricaoId, DateTime vencimento, decimal valor, ESituacao situacao)
        {
            Id = id;
            InscricaoId = inscricaoId;
            Vencimento = vencimento;
            Valor = valor;
            Situacao = situacao;
        }

        public string Id { get; }
        public string InscricaoId { get; }
        public DateTime Vencimento { get; }
        public decimal Valor { get; }
        public ESituacao Situacao { get; }

        public static Divida Criar(string inscricaoId, DateTime vencimento, decimal valor)
        {
            return new Divida(Guid.NewGuid().ToString(), inscricaoId, vencimento, valor, ESituacao.Aberta);
        }

        public enum ESituacao
        {
            Aberta,
            Paga,
            Anulada
        }
    }
}
