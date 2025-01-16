using System.ComponentModel.DataAnnotations;

namespace MiniPorjet.Models
{
    public class Intervention
    {
        [Key]
        public int InterventionId { get; set; }


        public DateTime InterventionDate { get; set; }

        public int ReclamationId { get; set; }
        public Reclamation Reclamation { get; set; }
        public string Statut { get; set; } // "Planifiée", "En cours", "Terminée"

        public bool EstGratuite { get; set; } // Indique si l'intervention est gratuite (sous garantie)

        public decimal? MontantFacture { get; set; } // Montant total facturé (si hors garantie)

        public string Description { get; set; } // Description des travaux effectués

    }
}
