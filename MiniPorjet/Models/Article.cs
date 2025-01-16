namespace MiniPorjet.Models
{
    public class Article
    {
        public int ArticleID { get; set; }
        public string ArticleName { get; set; }
        public string ArticleType { get; set; }

        public ICollection<Piece> Pieces { get; set; }
        public ICollection<Reclamation> Reclamations { get; set; }




    }
}
