namespace MiniPorjet.Models
{
    public class ClientArticle
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public DateTime DateAchat { get; set; }

        // Calcul automatique de la garantie (exemple : 2 ans)
        public bool SousGarantie => DateTime.Now < DateAchat.AddYears(2);
    }
}
