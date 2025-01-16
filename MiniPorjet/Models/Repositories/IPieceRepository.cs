namespace MiniPorjet.Models.Repositories
{
    public interface IPieceRepository
    {


        IEnumerable<Piece> GetAll();
        Piece GetById(int id);


        void Add(Piece piece);
        void Update(Piece piece);
        void Delete(int id);

        IList<Piece> GetPiecesByArticleID(int? articleId);
        Piece FindByName(string name);

        int GetQuantiteDispo(int pieceId);
        decimal GetPrix(int pieceId);






    }

}
