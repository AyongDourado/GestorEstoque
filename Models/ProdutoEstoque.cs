using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorEstoque.Models
{
    public class ProdutoEstoque
    {
        public int Id { get; set; }

        public int ProdutoId { get; set; }

        public Produto? Produto { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Validade { get; set; }
    }

}
