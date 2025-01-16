namespace MiniPorjet.Models.Repositories
{
    public interface IArticleRepository
    {
        IEnumerable<Article> GetAll();
        Article GetById(int id);
        void Add(Article article);
        void Update(Article article);
        void Delete(Article article);
        IEnumerable<Piece> GetPieces(int id);


    }
}
