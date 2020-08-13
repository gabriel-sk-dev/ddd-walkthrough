using System;
using System.ComponentModel.DataAnnotations;

namespace Escolas.API.Models
{
    public class TurmaInputModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Descrição muito grande")]
        public string Descricao { get; set; }
        [Required]
        [Range(1, 99, ErrorMessage = "Limite de alunos deve ser entre 1 e 99")]
        public int LimiteAlunos { get; set; }
        [Required]
        [Range(1, 99, ErrorMessage = "Idade mínima deve ser entre 1 e 99")]
        public int IdadeMinima { get; set; }
        public bool Aberta { get; set; }
    }
}
