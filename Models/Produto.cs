using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorEstoque.Models
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

        public int? QtdMin { get; set; }

        [NotMapped]
        public string ValorString { get; set; }

    }
}
