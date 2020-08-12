using System;
using System.ComponentModel.DataAnnotations;

namespace Escolas.API.Models
{
    public class Inscricao
    {
        public string Id { get; set; }
        public string AlunoId { get; set; }
        public DateTime InscritoEm { get; set; }
        [Required]
        public string TurmaId { get; set; }
    }
}
