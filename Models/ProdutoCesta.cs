using System.ComponentModel.DataAnnotations;

namespace GestorEstoque.Models
{
    public class ProdutoCesta
    {
        [Required]
        public Produto produto { get; set; }

        [Required]
        public int Quantidade { get; set; }
    }
}
