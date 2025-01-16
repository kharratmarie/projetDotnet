using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniPorjet.Context;
using MiniPorjet.Models;
using MiniPorjet.Models.Repositories;

namespace MiniPorjet.Controllers
{
    public class InterventionController : Controller
    {

        private readonly IInterventionRepository _interventionRepository;


        private readonly ApplicationDbContext _context;
        public InterventionController(ApplicationDbContext context, IInterventionRepository _interventionRepository)
        {
            _context = context;
            this._interventionRepository = _interventionRepository;
        }


        [AllowAnonymous]

        // GET: Reclamations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Interventions.Include(r => r.Reclamation);   // Assurez-vous que 'Piece' est chargé

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reclamations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervension = await _context.Interventions
                .Include(r => r.Reclamation)
                // Assurez-vous que 'Piece' est chargé

                .FirstOrDefaultAsync(m => m.ReclamationId == id);
            if (intervension == null)
            {
                return NotFound();
            }

            return View(intervension);
        }

        // GET: Reclamations/Create
        public IActionResult Create()
        {
            var Recalamations = _context.Reclamations.ToList();
            ViewBag.ReclamationId = new SelectList(Recalamations, "ReclamationId", "ReclamationId");

            return View();
        }

        // POST: Reclamations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Intervention/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReclamationId,InterventionDate,Description,Statut")] Intervention intervention)
        {
            try
            {

                // Récupérer la réclamation associée
                var reclamation = await _context.Reclamations
                    .Include(r => r.Article)
                    .Include(r => r.Client)
                    .Include(r => r.Piece)
                    .FirstOrDefaultAsync(r => r.ReclamationId == intervention.ReclamationId);

                if (reclamation == null)
                {
                    return NotFound();
                }


                if (_interventionRepository.IsInterventionFree(reclamation))
                {
                    intervention.EstGratuite = true;
                    intervention.MontantFacture = 0; // Gratuit sous garantie
                }
                else


                {
                    intervention.EstGratuite = false;

                    // Utilisation de la méthode CalculateInterventionCost pour calculer le coût de l'intervention
                    decimal tarifMainOeuvre = 50.00m; // Exemple de tarif de main-d'œuvre, vous pouvez l'adapter
                    decimal tauxTVA = 20.00m; // Exemple de taux de TVA, vous pouvez l'adapter
                    intervention.MontantFacture = _interventionRepository.CalculateInterventionCost(
                    intervention, tarifMainOeuvre, tauxTVA);

                }
                intervention.Statut = "fjk";
                intervention.Description = "sdfjklm";

                _context.Add(intervention);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["ReclamationId"] = new SelectList(_context.Reclamations, "ReclamationId", "ReclamationDescription", intervention.ReclamationId);
                return View(intervention);
            }
        }

        // GET: Intervention/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervention = await _context.Interventions.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }

            ViewData["ReclamationId"] = new SelectList(_context.Reclamations, "ReclamationId", "ReclamationDescription", intervention.ReclamationId);
            return View(intervention);
        }

        // POST: Intervention/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InterventionId,InterventionDate,ReclamationId,Statut , Description")] Intervention intervention)
        {
            if (id != intervention.InterventionId)
            {
                

                return NotFound();
            }

           
                try
                {

                    intervention.Statut = "terminé";
                    intervention.Description = "sdfjklm";

                    
                    _context.Update(intervention);
                    await _context.SaveChangesAsync();

                    if (intervention.Statut == "terminé")
                {
                    var reclamation = await _context.Reclamations
                   .Include(r => r.Article)
                   .Include(r => r.Client)
                   .Include(r => r.Piece)
                   .FirstOrDefaultAsync(r => r.ReclamationId == intervention.ReclamationId);
                    if (reclamation != null)
                    {
                        // Update the reclamation's status
                        reclamation.ReclamationStatut = "Résolu";
                        _context.Update(reclamation); // Pass the single entity
                        await _context.SaveChangesAsync();
                    }
                }


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterventionExists(intervention.InterventionId))
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
        // GET: Intervention/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervention = await _context.Interventions
                .Include(i => i.Reclamation) // Inclure les détails de la réclamation liée
                .FirstOrDefaultAsync(i => i.InterventionId == id);

            if (intervention == null)
            {
                return NotFound();
            }

            return View(intervention);
        }

        // POST: Intervention/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intervention = await _context.Interventions.FindAsync(id);
            if (intervention != null)
            {
                _context.Interventions.Remove(intervention);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        // Vérification de l'existence d'une intervention
        private bool InterventionExists(int id)
        {
            return _context.Interventions.Any(e => e.InterventionId == id);
        }
    }
    }

