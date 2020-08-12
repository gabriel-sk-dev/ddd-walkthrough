using System;
using System.ComponentModel.DataAnnotations;

namespace Escolas.API.Models
{
    public class InscricaoInputModel
    {
        [Required]
        public string TurmaId { get; set; }
        [Required]
        public string AlunoId { get; set; }
    }
}
