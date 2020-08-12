using System;
using System.ComponentModel.DataAnnotations;

namespace Escolas.API.Models
{
    public class AlunoInputModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Nome muito grande")]
        public string Nome { get; set; }
        [Required]
        [RegularExpression(@"^(.+)@(.+)$", ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
