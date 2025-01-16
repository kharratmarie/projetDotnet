using MiniPorjet.Context;

namespace MiniPorjet.Models.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        readonly ApplicationDbContext context;
        public ArticleRepository(ApplicationDbContext context)
        {

            this.context = context;
        }

     
        public Article GetById(int id)
        {
            return context.Articles.Find(id);
        }
        public void Add(Article s)
        {
            context.Articles.Add(s);
            context.SaveChanges();
        }
     

        public IEnumerable<Article> GetAll()
        {
            return context.Articles.OrderBy(s => s.ArticleName).ToList();
        }

        public void Update(Article s)
        {

            Article s1 = context.Articles.Find(s.ArticleID);
            if (s1 != null)
            {
                s1.ArticleName = s.ArticleName;
                s1.ArticleType = s.ArticleType;
                context.SaveChanges();
            }
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

        
    }
}
