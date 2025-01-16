namespace MiniPorjet.Models
{
    public class Reclamation
    {
        public int ReclamationId { get; set; }
  
        public string ReclamationDescription { get; set; }
        public string ReclamationStatut { get; set; } // "Nouvelle", "En cours", "Résolue"
        public DateTime ReclamationDate { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public int PieceId { get; set; }
        public Piece Piece { get; set; }

    }
}
