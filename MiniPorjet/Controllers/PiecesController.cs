using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniPorjet.Context;
using MiniPorjet.Models;

namespace MiniPorjet.Controllers
{
    [Authorize(Roles = "ResponsableSAV")]

    public class PiecesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PiecesController(ApplicationDbContext context)
        {
            _context = context;
        }
                [AllowAnonymous]


        // GET: Pieces
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pieces.Include(p => p.Article);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pieces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piece = await _context.Pieces
                .Include(p => p.Article)
                .FirstOrDefaultAsync(m => m.PieceId == id);
            if (piece == null)
            {
                return NotFound();
            }

            return View(piece);
        }

        // GET: Pieces/Create
        public IActionResult Create()
        {
            ViewData["ArticleID"] = new SelectList(_context.Articles, "ArticleID", "ArticleID");
            return View();
        }

        // POST: Pieces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PieceId,PieceName,PiecePrix,QuantiteDispo,ArticleID")] Piece piece)
        {
try            {
                _context.Add(piece);
                await _context.SaveChangesAsync();
                ViewData["ArticleID"] = new SelectList(_context.Articles, "ArticleID", "ArticleID", piece.ArticleID);
                return RedirectToAction(nameof(Index));

            }
            catch {
            return View(piece);
            }
        }

        // GET: Pieces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piece = await _context.Pieces.FindAsync(id);
            if (piece == null)
            {
                return NotFound();
            }
            ViewData["ArticleID"] = new SelectList(_context.Articles, "ArticleID", "ArticleID", piece.ArticleID);
            return View(piece);
        }

        // POST: Pieces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PieceId,PieceName,PiecePrix,QuantiteDispo,ArticleID")] Piece piece)
        {
            if (id != piece.PieceId)
            {
                return NotFound();
            }

            try
            {
                try
                {
                    _context.Update(piece);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PieceExists(piece.PieceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["ArticleID"] = new SelectList(_context.Articles, "ArticleID", "ArticleID", piece.ArticleID);

                return RedirectToAction(nameof(Index));
            }
            catch { 
            return View(piece);
            }
        }

        // GET: Pieces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piece = await _context.Pieces
                .Include(p => p.Article)
                .FirstOrDefaultAsync(m => m.PieceId == id);
            if (piece == null)
            {
                return NotFound();
            }

            return View(piece);
        }

        // POST: Pieces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var piece = await _context.Pieces.FindAsync(id);
            if (piece != null)
            {
                _context.Pieces.Remove(piece);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PieceExists(int id)
        {
            return _context.Pieces.Any(e => e.PieceId == id);
        }
        [HttpGet]
        public ActionResult Search(int? articleId)
        {
            if (articleId == null)
            {
                ViewBag.Message = "Veuillez entrer un article pour effectuer la recherche.";
                return View("Index", new List<Piece>());
            }

            var result = _context.Pieces
                .Include(r => r.Article)
                .Where(r => r.ArticleID == articleId)
                .ToList();

            if (!result.Any())
            {
                ViewBag.Message = "Aucune réclamation trouvée pour cet article.";
            }

            return View("Index", result);
        }

    }
}
