using System;
using System.ComponentModel.DataAnnotations;

namespace Escolas.API.Models
{
    public class NovoAlunoInputModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Nome muito grande")]
        public string Nome { get; set; }
        [Required]
        [RegularExpression(@"^(.+)@(.+)$", ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [Required]
        public string Sexo { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public int DistanciaAteEscola { get; set; }
    }
}
