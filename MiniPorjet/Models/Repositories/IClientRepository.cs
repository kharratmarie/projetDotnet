namespace MiniPorjet.Models.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAll();
        Client GetById(int id);
        Client FindByName(string name);


        void Add(Client Client);
        void Update(Client Client);
        void Delete(Client Client);
        IEnumerable<Reclamation> GetReclamations(int id);
    }
}
