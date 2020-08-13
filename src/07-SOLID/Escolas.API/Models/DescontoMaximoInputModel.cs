using System.ComponentModel.DataAnnotations;

namespace Escolas.API.Models
{
    public class DescontoMaximoInputModel
    {
        [Required]
        public decimal Valor { get; set; }
    }
}
