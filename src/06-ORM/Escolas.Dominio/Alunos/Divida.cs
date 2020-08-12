using System;

namespace Escolas.Dominio.Alunos
{
    public sealed class Divida
    {
        private Divida() { }
        private Divida(string id, string alunoId, string inscricaoId, DateTime vencimento, decimal valor, ESituacao situacao)
        {
            Id = id;
            AlunoId = alunoId;
            InscricaoId = inscricaoId;
            Vencimento = vencimento;
            Valor = valor;
            Situacao = situacao;
        }

        public string Id { get; }
        public string AlunoId { get; }
        public string InscricaoId { get; }
        public DateTime Vencimento { get; }
        public decimal Valor { get; }
        public ESituacao Situacao { get; }

        public static Divida Criar(Inscricao inscricao, DateTime vencimento, decimal valor)
        {
            return new Divida(Guid.NewGuid().ToString(), inscricao.AlunoId, inscricao.Id, vencimento, valor, ESituacao.Aberta);
        }

        public enum ESituacao
        {
            Aberta,
            Paga,
            Anulada
        }
    }
}
