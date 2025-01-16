using MiniPorjet.Context;

namespace MiniPorjet.Models.Repositories
{
    public class ClientRepository : IClientRepository
    {
        readonly ApplicationDbContext context;
        public ClientRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


      

      
     

        public void Delete(Article s)
        {
            Article s1 = context.Articles.Find(s.ArticleID);
            if (s1 != null)
            {
                context.Articles.Remove(s1);
                context.SaveChanges();
            }
        }

        public IEnumerable<Piece> GetPieces(int id)
        {
            // Trouver l'article par ID et inclure les pièces associées
            return context.Articles
                          .Where(a => a.ArticleID == id)
                          .SelectMany(a => a.Pieces)
                          .ToList();
        }

        public IEnumerable<Client> GetAll()
        {
            return context.Clients.OrderBy(s => s.ClientName);
        }

        public Client GetById(int id)
        {
            return context.Clients.Find(id);
        }

        public Client FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Add(Client Client)
        {

            context.Clients.Add(Client);
            context.SaveChanges();
        }

        public void Update(Client Client)
        {

            Client s1 = context.Clients.Find(Client.ClientId);
            if (s1 != null)
            {
                s1.ClientName = Client.ClientName;
                s1.ClientTelephone = Client.ClientTelephone;
                s1.ClientAdresse = Client.ClientAdresse;

                context.SaveChanges();
            }
        }

        public void Delete(Client s)
        {
            Client s1 = context.Clients.Find(s.ClientId);
            if (s1 != null)
            {
                context.Clients.Remove(s1);
                context.SaveChanges();
            }
        }

        public IEnumerable<Reclamation> GetReclamations(int id)
        {
            // Trouver l'article par ID et inclure les pièces associées
            return context.Clients
                          .Where(a => a.ClientId == id)
                          .SelectMany(a => a.Reclamations)
                          .ToList();
        }
    }
}
