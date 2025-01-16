using System.ComponentModel.DataAnnotations;

namespace MiniPorjet.Models
{
    public class Piece
    {
        public int PieceId { get; set; }
        [Required]
        public string PieceName { get; set; }
        public int PiecePrix { get; set; }
        public int QuantiteDispo { get; set; }


        public int ArticleID { get; set; }
        public Article Article { get; set; }

        public ICollection<Reclamation> Reclamations { get; set; }

    }
}
