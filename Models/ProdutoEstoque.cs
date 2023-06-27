using System.ComponentModel.DataAnnotations;

namespace StockManager1.Models
{
    public class ProdutoEstoque
    {
        public int Id { get; set; }

        [Required]
        public Produto produto { get; set; }

        [Required]
        public int Quantidade { get; set; }

        public int? QtdMin { get; set; }
    }

}
