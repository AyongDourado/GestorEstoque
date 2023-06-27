using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestorEstoque.Data;
using StockManager1.Models;

namespace GestorEstoque.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly GestorEstoqueContext _context;

        public EstoqueController(GestorEstoqueContext context)
        {
            _context = context;
        }

        // GET: Estoque
        public async Task<IActionResult> Index()
        {
              return _context.ProdutoEstoque != null ? 
                          View(await _context.ProdutoEstoque.ToListAsync()) :
                          Problem("Entity set 'GestorEstoqueContext.ProdutoEstoque'  is null.");
        }

        // GET: Estoque/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProdutoEstoque == null)
            {
                return NotFound();
            }

            var produtoEstoque = await _context.ProdutoEstoque
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtoEstoque == null)
            {
                return NotFound();
            }

            return View(produtoEstoque);
        }

        // GET: Estoque/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estoque/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantidade,QtdMin")] ProdutoEstoque produtoEstoque)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produtoEstoque);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produtoEstoque);
        }

        // GET: Estoque/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProdutoEstoque == null)
            {
                return NotFound();
            }

            var produtoEstoque = await _context.ProdutoEstoque.FindAsync(id);
            if (produtoEstoque == null)
            {
                return NotFound();
            }
            return View(produtoEstoque);
        }

        // POST: Estoque/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantidade,QtdMin")] ProdutoEstoque produtoEstoque)
        {
            if (id != produtoEstoque.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtoEstoque);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoEstoqueExists(produtoEstoque.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produtoEstoque);
        }

        // GET: Estoque/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProdutoEstoque == null)
            {
                return NotFound();
            }

            var produtoEstoque = await _context.ProdutoEstoque
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtoEstoque == null)
            {
                return NotFound();
            }

            return View(produtoEstoque);
        }

        // POST: Estoque/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProdutoEstoque == null)
            {
                return Problem("Entity set 'GestorEstoqueContext.ProdutoEstoque'  is null.");
            }
            var produtoEstoque = await _context.ProdutoEstoque.FindAsync(id);
            if (produtoEstoque != null)
            {
                _context.ProdutoEstoque.Remove(produtoEstoque);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoEstoqueExists(int id)
        {
          return (_context.ProdutoEstoque?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult TesteUsuario()
        {
            return View();
        }
    }
}
