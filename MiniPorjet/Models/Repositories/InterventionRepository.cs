using Microsoft.EntityFrameworkCore;
using MiniPorjet.Context;

namespace MiniPorjet.Models.Repositories
{
    public class InterventionRepository : IInterventionRepository
    {
        readonly ApplicationDbContext context;

        public InterventionRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Intervention Intervention)
        {
            context.Interventions.Add(Intervention);
            context.SaveChanges();
        }

        public decimal CalculateInterventionCost(Intervention intervention, decimal tarifMainOeuvre, decimal tauxTVA)
        {
            // Rechercher l'intervention par son ID
            //var intervention = context.Interventions
            //    .Include(i => i.Reclamation) // Inclure les relations nécessaires
            //    .ThenInclude(r => r.Piece)  // Inclure la pièce associée
            //    .FirstOrDefault(i => i.InterventionId == interventionId);

            if (intervention == null)
            {
                throw new ArgumentException("L'intervention spécifiée est introuvable.");
            }

            // Vérifier si l'intervention est gratuite
            if (IsInterventionFree(intervention.Reclamation))
            {
                return 0; // Gratuit si sous garantie
            }

            // Si hors garantie, calculer le coût
            decimal pieceCost = intervention.Reclamation.Piece?.PiecePrix ?? 0; // Gérer le cas où la pièce est null
            decimal cost = pieceCost + tarifMainOeuvre;

            // Appliquer la TVA
            return cost + (cost * tauxTVA / 100);
        }


        public void Delete(int id)
        {
            var Intervention = context.Interventions.Find(id);
            if (Intervention != null)
            {
                context.Interventions.Remove(Intervention);
                context.SaveChanges();
            }
        }


        public IEnumerable<Intervention> GetAll()
        {
            return context.Interventions
                .OrderBy(r => r.InterventionDate)
                .ToList();
        }

        public Intervention GetById(int id)
        {
            return context.Interventions.Find(id);
        }





        public IEnumerable<Intervention> GetInterventionsByReclamation(int reclamationId)
        {
            return context.Interventions.Where(i => i.ReclamationId == reclamationId).ToList();
        }

        public IEnumerable<Intervention> GetInterventionsByStatus(string status)
        {
            return context.Interventions.Where(i => i.Statut == status).ToList();
        }

        public bool IsInterventionFree(Reclamation reclamation)
        {
            if (reclamation == null)
            {
                throw new ArgumentException("La réclamation ne peut pas être nulle.");
            }

            // Récupérer le clientId et l'articleId à partir de la réclamation
            var clientId = reclamation.ClientId;
            var articleId = reclamation.ArticleId;

            // Rechercher l'article associé au client dans la table ClientArticles
            var clientArticle = context.ClientArticles
                .FirstOrDefault(ca => ca.ClientId == clientId && ca.ArticleId == articleId);

            // Vérifier si l'article est trouvé et retourner le statut de garantie
            return clientArticle?.SousGarantie ?? false; // Retourne false si clientArticle est null
        }

        public void Update(Intervention intervention)
        {
            var existingIntervention = context.Interventions.Find(intervention.InterventionId);
            if (existingIntervention != null)
            {

                existingIntervention.InterventionDate = intervention.InterventionDate;
                existingIntervention.ReclamationId = intervention.ReclamationId;



                context.SaveChanges();
            }
        }
    }
}
