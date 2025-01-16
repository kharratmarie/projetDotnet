namespace MiniPorjet.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
     
        public string ClientAdresse { get; set; }
        public string ClientTelephone { get; set; }
        public ICollection<Reclamation> Reclamations { get; set; }
    }
}
