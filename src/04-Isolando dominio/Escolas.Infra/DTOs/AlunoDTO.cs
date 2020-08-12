using System;
using System.Collections.Generic;
using System.Text;

namespace Escolas.Infra.DTOs
{
    public class AlunoDTO
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
    }

    public class TurmaDTO
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public int LimiteAlunos { get; set; }
        public int IdadeMinima { get; set; }
        public int TotalInscritos { get; set; }
        public int DuracaoEmMeses { get; set; }
        public decimal ValorMensal { get; set; }
        public bool Aberta { get; set; }
    }

    public class InscricaoDTO
    {
        public string Id { get; set; }
        public string AlunoId { get; set; }
        public string TurmaId { get; set; }
        public DateTime InscritoEm { get; set; }
    }

    public class DividaDTO
    {
        public string Id { get; set; }
        public string InscricaoId { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal Valor { get; set; }
        public string Situacao { get; set; }
    }
}
