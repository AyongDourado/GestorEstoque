using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestorEstoque.Data;
using GestorEstoque.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestorEstoque.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProdutoEstoqueController : Controller
    {
        private readonly GestorEstoqueContext _context;

        public ProdutoEstoqueController(GestorEstoqueContext context)
        {
            _context = context;
        }

        // GET: ProdutoEstoque
        public async Task<IActionResult> Index()
        {
            var gestorEstoqueContext = _context.ProdutoEstoque.Include(p => p.Produto);
            return View(await gestorEstoqueContext.ToListAsync());
        }

        // GET: ProdutoEstoque/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProdutoEstoque == null)
            {
                return NotFound();
            }

            var produtoEstoque = await _context.ProdutoEstoque
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtoEstoque == null)
            {
                return NotFound();
            }

            return View(produtoEstoque);
        }

        // GET: ProdutoEstoque/Create
        public IActionResult Create()
        {
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Nome");
            return View();
        }

        // POST: ProdutoEstoque/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProdutoId,Produto,Quantidade,Validade")] ProdutoEstoque produtoEstoque)
        {
            var produto = await _context.Produto.FindAsync(produtoEstoque.ProdutoId);
            produtoEstoque.Produto = produto;
            if (ModelState.IsValid)
            {
                _context.Add(produtoEstoque); //pega produto estoque salva na memória
                await _context.SaveChangesAsync(); //e salva no banco
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Nome", produtoEstoque.ProdutoId);
            return View(produtoEstoque);
        }

        // GET: ProdutoEstoque/Edit/5
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
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Nome", produtoEstoque.ProdutoId);
            return View(produtoEstoque);
        }

        // POST: ProdutoEstoque/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProdutoId,Quantidade,Validade")] ProdutoEstoque produtoEstoque)
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
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Nome", produtoEstoque.ProdutoId);
            return View(produtoEstoque);
        }

        // GET: ProdutoEstoque/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProdutoEstoque == null)
            {
                return NotFound();
            }

            var produtoEstoque = await _context.ProdutoEstoque
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtoEstoque == null)
            {
                return NotFound();
            }

            return View(produtoEstoque);
        }

        // POST: ProdutoEstoque/Delete/5
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
    }
}
