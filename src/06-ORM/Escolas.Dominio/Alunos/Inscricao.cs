using Escolas.Dominio.Turmas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Escolas.Dominio.Alunos
{
    public sealed class Inscricao
    {
        private Inscricao() { }
        private Inscricao(string id, string alunoId, Turma turma, DateTime inscritoEm)
        {
            Id = id;
            AlunoId = alunoId;
            Turma = turma;
            InscritoEm = inscritoEm;
        }

        public string Id { get; }
        public string AlunoId { get; }
        public Turma Turma { get; }
        public DateTime InscritoEm { get; }

        public static Inscricao Criar(string alunoId, Turma turma)
        {
            return new Inscricao(Guid.NewGuid().ToString(), alunoId, turma, DateTime.Now);
        }

        public IEnumerable<Divida> GerarDividas()
        {
            var vencimento = InscritoEm.AddMonths(1);
            for (int i = 0; i < Turma.DuracaoEmMeses; i++)
            {
                vencimento.AddMonths(i);
                yield return Divida.Criar(this, vencimento, Turma.ValorMensal);
            }
        }
    }
}
