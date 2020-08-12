using System;
using System.ComponentModel.DataAnnotations;

namespace Escolas.API.Models
{
    public class Turma
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Descrição muito grande")]
        public string Descricao { get; set; }
    }
}
