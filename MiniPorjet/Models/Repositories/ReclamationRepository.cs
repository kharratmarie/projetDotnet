using System;
using System.Collections.Generic;
using System.Linq;
using MiniPorjet.Context;

namespace MiniPorjet.Models.Repositories
{
    public class ReclamationRepository : IReclamationRepository
    {
        readonly ApplicationDbContext context;

        public ReclamationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Reclamation reclamation)
        {
            context.Reclamations.Add(reclamation);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var reclamation = context.Reclamations.Find(id);
            if (reclamation != null)
            {
                context.Reclamations.Remove(reclamation);
                context.SaveChanges();
            }
        }

        public IList<Reclamation> FindByStatus(string status)
        {
            return context.Reclamations
                .Where(r => r.ReclamationStatut.Equals(status, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public IEnumerable<Reclamation> GetAll()
        {
            return context.Reclamations
                .OrderBy(r => r.ReclamationDate)
                .ToList();
        }

        public Reclamation GetById(int id)
        {
            return context.Reclamations.Find(id);
        }

        public IList<Reclamation> GetReclamationByArticleID(int? articleId)
        {
            if (articleId == null)
                return new List<Reclamation>();

            return context.Reclamations
                .Where(r => r.ArticleId == articleId)
                .ToList();
        }
        public IList<Reclamation> GetReclamationByPieceID(int? PieceId)
        {
            if (PieceId == null)
                return new List<Reclamation>();

            return context.Reclamations
                .Where(r => r.PieceId == PieceId)
                .ToList();
        }
        public IList<Reclamation> GetReclamationByClientID(int? clientId)
        {
            if (clientId == null)
                return new List<Reclamation>();

            return context.Reclamations
                .Where(r => r.ClientId == clientId)
                .ToList();
        }

        public void Update(Reclamation reclamation)
        {
            var existingReclamation = context.Reclamations.Find(reclamation.ReclamationId);
            if (existingReclamation != null)
            {
                existingReclamation.ReclamationDescription = reclamation.ReclamationDescription;
                existingReclamation.ReclamationStatut = reclamation.ReclamationStatut;
                existingReclamation.ReclamationDate = reclamation.ReclamationDate;
                existingReclamation.ClientId = reclamation.ClientId;
                existingReclamation.ArticleId = reclamation.ArticleId;
                existingReclamation.PieceId = reclamation.PieceId;


                context.SaveChanges();
            }
        }
    }
}
