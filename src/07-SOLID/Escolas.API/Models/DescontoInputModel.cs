using System.ComponentModel.DataAnnotations;

namespace Escolas.API.Models
{
    public class DescontoInputModel
    {
        [Required]
        public string Regra { get; set; }
        [Required]
        public decimal Valor { get; set; }
        public int LimiteDistancia { get; set; }
    }
}
