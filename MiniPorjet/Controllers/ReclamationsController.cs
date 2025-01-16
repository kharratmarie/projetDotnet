using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniPorjet.Context;
using MiniPorjet.Models;
using MiniPorjet.Models.Repositories;

namespace MiniPorjet.Controllers
{
    [Authorize(Roles = "Client")]

    public class ReclamationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> userManager;

        public ReclamationsController(UserManager<IdentityUser>
     userManager,ApplicationDbContext context)
        {
            this.userManager = userManager;
            _context = context;
        }


       
        [AllowAnonymous]

        // GET: Reclamations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reclamations.Include(r => r.Article).Include(r => r.Client).Include(r => r.Piece);   // Assurez-vous que 'Piece' est chargé

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reclamations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclamation = await _context.Reclamations
                .Include(r => r.Article)
                .Include(r => r.Client)
                .Include(r => r.Piece)   // Assurez-vous que 'Piece' est chargé

                .FirstOrDefaultAsync(m => m.ReclamationId == id);
            if (reclamation == null)
            {
                return NotFound();
            }

            return View(reclamation);
        }

        // GET: Reclamations/Create
        public async Task<IActionResult> Create()
        {
            // Récupérer l'utilisateur connecté
            var user = await userManager.GetUserAsync(User);
         
            if (user == null)
            {
                // Si l'utilisateur n'est pas connecté, rediriger ou afficher un message d'erreur
                return Unauthorized(); // Exemple : rediriger vers la page de connexion
            }

            // Trouver le client associé à l'utilisateur
            var client = _context.Clients.FirstOrDefault(c => c.ClientTelephone == user.PhoneNumber);

            if (client == null)
            {
                // Si aucun client n'est trouvé, retourner une erreur ou afficher un message
                return NotFound("Client not found for the current user.");
            }

            // Filtrer les articles associés au client
            var articles = _context.ClientArticles.Where(a => a.ClientId == client.ClientId).ToList();

            // Passer les articles et le ClientId à la vue
            ViewBag.ArticleId = new SelectList(articles, "ArticleId", "ArticleId");
            ViewBag.ClientId = client.ClientId;

            return View();
        }


        // POST: Reclamations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reclamation reclamation)
        {
            try
            {
                // Ajouter l'ID du client à la réclamation
                var user = await userManager.GetUserAsync(User);
                var client = _context.Clients.FirstOrDefault(c => c.ClientTelephone == user.PhoneNumber);
                reclamation.ClientId = client.ClientId;
                
                _context.Add(reclamation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(reclamation);
            }
        }

        [AllowAnonymous]

        // GET: Reclamations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclamation = await _context.Reclamations.FindAsync(id);
            if (reclamation == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles, "ArticleID", "ArticleID", reclamation.ArticleId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", reclamation.ClientId);
            ViewData["PieceId"] = new SelectList(_context.Pieces, "PieceId", "PieceId", reclamation.PieceId);

            return View(reclamation);
        }

        // POST: Reclamations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReclamationId,ReclamationDescription,ReclamationStatut,ReclamationDate,ClientId,ArticleId,PieceId")] Reclamation reclamation)
        {
            if (id != reclamation.ReclamationId)
            {
                return NotFound();
            }

            try {
                
                    try
                    {
                        _context.Update(reclamation);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ReclamationExists(reclamation.ReclamationId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    ViewData["ArticleId"] = new SelectList(_context.Articles, "ArticleID", "ArticleID", reclamation.ArticleId);
                    ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", reclamation.ClientId);

                    return RedirectToAction(nameof(Index));
                }catch {
                return View(reclamation); }
        }

        // GET: Reclamations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reclamation = await _context.Reclamations
                .Include(r => r.Article)
                .Include(r => r.Client)
                .FirstOrDefaultAsync(m => m.ReclamationId == id);
            if (reclamation == null)
            {
                return NotFound();
            }

            return View(reclamation);
        }

        // POST: Reclamations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reclamation = await _context.Reclamations.FindAsync(id);
            if (reclamation != null)
            {
                _context.Reclamations.Remove(reclamation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReclamationExists(int id)
        {
            return _context.Reclamations.Any(e => e.ReclamationId == id);
        }

        [HttpGet]
        public IActionResult GetPieces(int articleId)
        {
            var pieces = _context.Pieces
                .Where(p => p.ArticleID == articleId)
                .Select(p => new
                {
                    pieceId = p.PieceId,
                    pieceName = p.PieceName
                })
                .ToList();

            return Json(pieces);
        }
        [AllowAnonymous]

        [HttpGet]
        public ActionResult Search(int? articleId)
        {
            if (articleId == null)
            {
                ViewBag.Message = "Veuillez entrer un article pour effectuer la recherche.";
                return View("Index", new List<Reclamation>());
            }

            var result = _context.Reclamations
                .Include(r => r.Article)
                .Include(r => r.Client)
                .Include(r => r.Piece)
                .Where(r => r.ArticleId == articleId)
                .ToList();

            if (!result.Any())
            {
                ViewBag.Message = "Aucune réclamation trouvée pour cet article.";
            }

            return View("Index", result);
        }


    }
}
