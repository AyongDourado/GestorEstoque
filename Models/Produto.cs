using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager1.Models
{
    public class Produto
    {
        public int Id { get; set; }

        public string? Marca { get; set;}

        [Required]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [NotMapped]
        public string ValorString { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Validade { get; set; }
    }
}
