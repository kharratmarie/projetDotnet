using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniPorjet.Context;
using MiniPorjet.Models;

namespace MiniPorjet.Controllers
{
    [Authorize(Roles = "ResponsableSAV")]

    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public ArticlesController(UserManager<IdentityUser>
     userManager , ApplicationDbContext context)
        {
            _context = context;
            this.userManager = userManager; 
        }
        [AllowAnonymous]
        // GET: Articles
        public async Task<IActionResult> Index()
        {
            // Récupérer l'utilisateur connecté
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                // Si l'utilisateur n'est pas connecté, rediriger ou afficher un message d'erreur
                return Unauthorized("You must be logged in to view articles.");
            }

            List<Article> articles;

            if (await userManager.IsInRoleAsync(user, "ResponsableSAV"))
            {
                // Si l'utilisateur est un ResponsableSAV, récupérer tous les articles
                articles = await _context.Articles.ToListAsync();
            }
            else
            {
                // Trouver le client associé à l'utilisateur
                var client = await _context.Clients
                    .FirstOrDefaultAsync(c => c.ClientTelephone == user.PhoneNumber);

                if (client == null)
                {
                    // Si aucun client n'est trouvé, retourner une erreur
                    return NotFound("Client not found for the current user.");
                }

                // Récupérer les articles associés au client
                articles = await _context.ClientArticles
                    .Where(ca => ca.ClientId == client.ClientId)
                    .Select(ca => ca.Article)
                    .ToListAsync();
            }

            // Retourner la vue avec les articles
            return View(articles);
        }
        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.ArticleID == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }


        // GET: Articles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleID,ArticleName,ArticleType")] Article article)
        {
            try
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(article);
            }
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleID,ArticleName,ArticleType")] Article article)
        {
            if (id != article.ArticleID)
            {
                return NotFound();
            }

            try
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.ArticleID))
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
            catch { 
            return View(article);
            }
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.ArticleID == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.ArticleID == id);
        }
    }
}
