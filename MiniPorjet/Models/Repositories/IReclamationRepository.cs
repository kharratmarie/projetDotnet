namespace MiniPorjet.Models.Repositories
{
    public interface IReclamationRepository
    {

        IEnumerable<Reclamation> GetAll();
        Reclamation GetById(int id);


        void Add(Reclamation Reclamation);
        void Update(Reclamation Reclamation);
        void Delete(int id);

        IList<Reclamation> GetReclamationByArticleID(int? articleId);
        IList<Reclamation> GetReclamationByClientID(int? Client);

        IList<Reclamation> FindByStatus(string status);
        public IList<Reclamation> GetReclamationByPieceID(int? PieceId);



    }
}
