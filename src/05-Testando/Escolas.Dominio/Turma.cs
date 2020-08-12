using System;

namespace Escolas.Dominio
{
    public sealed class Turma
    {
        public Turma(string id, string descricao, int limiteAlunos, int idadeMinima, int totalInscritos, int duracaoEmMeses, decimal valorMensal, bool aberta)
        {
            Id = id;
            Descricao = descricao;
            LimiteAlunos = limiteAlunos;
            IdadeMinima = idadeMinima;
            TotalInscritos = totalInscritos;
            DuracaoEmMeses = duracaoEmMeses;
            ValorMensal = valorMensal;
            Aberta = aberta;
        }

        public string Id { get; }        
        public string Descricao { get; }        
        public int LimiteAlunos { get; }        
        public int IdadeMinima { get; }
        public int TotalInscritos { get; private set; }
        public int DuracaoEmMeses { get; }
        public decimal ValorMensal { get; }
        public bool Aberta { get; private set; }

        public static Turma CriarFechada(string descricao, int limiteAlunos, int idadeMinima, int duracaoEmMeses, decimal valorMensal)
        {
            return new Turma(Guid.NewGuid().ToString(), descricao, limiteAlunos, idadeMinima, 0, duracaoEmMeses, valorMensal, false);
        }

        public void AbrirParaInscricoes()
        {
            Aberta = true;
        }

        internal void AceitaInscricao(Aluno aluno)
        {
            if (aluno.IdadeHoje < IdadeMinima)
                throw new InvalidOperationException("Aluno não possui idade mínima para a turma");
            if ((TotalInscritos + 1) > LimiteAlunos)
                throw new InvalidOperationException("Turma não aceita mais incrições");
            if(!Aberta)
                throw new InvalidOperationException("Turma fechada para inscrições");
        }

        internal void ConfirmarInscricao()
        {
            TotalInscritos++;
        }

    }
}
