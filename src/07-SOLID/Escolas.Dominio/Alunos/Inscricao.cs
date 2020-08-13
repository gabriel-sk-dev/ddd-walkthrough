using Escolas.Dominio.Turmas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Escolas.Dominio.Alunos
{
    public sealed class Inscricao
    {
        private Inscricao() { }
        private Inscricao(string id, Aluno aluno, Turma turma, DateTime inscritoEm, ETipoPagamento tipoPagamento)
        {
            Id = id;
            Aluno = aluno;
            Turma = turma;
            InscritoEm = inscritoEm;
            TipoPagamento = tipoPagamento;
        }

        public string Id { get; }
        public Aluno Aluno { get; }
        public Turma Turma { get; }
        public DateTime InscritoEm { get; }
        public ETipoPagamento TipoPagamento { get; }

        public static Inscricao Criar(Aluno aluno, Turma turma, ETipoPagamento tipoPagamento)
        {
            return new Inscricao(Guid.NewGuid().ToString(), aluno, turma, DateTime.Now, tipoPagamento);
        }

        public IEnumerable<Divida> GerarDividas()
        {
            var vencimento = InscritoEm.AddMonths(1);
            for (int i = 0; i < Turma.ConfiguracaoInscricao.DuracaoEmMeses; i++)
            {
                vencimento.AddMonths(i);
                yield return Divida.Criar(this, vencimento, Turma.CalcularValorMensal(this));
            }
        }

        public enum ETipoPagamento
        {
            Mensal,
            Antecipado
        }
    }
}
