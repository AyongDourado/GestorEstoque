using GestorEstoque.Data;
using GestorEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorEstoque.Controllers
{
    public class LojaController : Controller
    {
        private readonly GestorEstoqueContext _context;

        public LojaController(GestorEstoqueContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.ProdutoEstoque != null ?
                    View(await _context.ProdutoEstoque.Include(pe => pe.Produto).GroupBy(pe => pe.ProdutoId).Select(pe=> pe.First()).ToListAsync()) :
                    Problem("Entity set 'GestorEstoqueContext.Produto'  is null.");
        }

        [HttpPost]
        public async Task<IActionResult> Comprar([FromBody] List<Cesta> cesta)
        {
            //Pegando do banco tudo que está na cesta
            var produtosEstoque = await _context.ProdutoEstoque.Include(pe => pe.Produto).ToListAsync();

            produtosEstoque = produtosEstoque.Where(pe => cesta.Any(ci => ci.Id == pe.Id)).ToList();

            foreach(var produtoEstoque in produtosEstoque) {
                var itemCesta = cesta.First(c => c.Id == produtoEstoque.Id);
                
                if(produtoEstoque.Quantidade == 0){
                    _context.Remove(produtoEstoque);
                }
                else if(produtoEstoque.Quantidade < itemCesta.Quantidade){
                    return Problem($"Estoque insuficiente de {produtoEstoque.Produto.Nome}, disponível apenas {produtoEstoque.Quantidade} unidades");
                }
                else{
                    produtoEstoque.Quantidade -= itemCesta.Quantidade;
                    _context.Update(produtoEstoque);
                }
            }

            await _context.SaveChangesAsync();

            return Redirect("/Home");
        }
    }
}
