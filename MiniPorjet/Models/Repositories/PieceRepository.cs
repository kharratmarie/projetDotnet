using MiniPorjet.Context;

namespace MiniPorjet.Models.Repositories
{
    public class PieceRepository : IPieceRepository
    {

        readonly ApplicationDbContext context;
        public PieceRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Piece piece)
        {
            context.Pieces.Add(piece);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Piece FindByName(string name)
        {
            return context.Pieces.FirstOrDefault(p => p.PieceName.Equals(name));
        }

        public IEnumerable<Piece> GetAll()
        {
            return context.Pieces.OrderBy(s => s.PieceName).ToList();
        }

        public Piece GetById(int id)
        {
            return context.Pieces.Find(id);
        }

        public IList<Piece> GetPiecesByArticleID(int? articleId)
        {
            if (articleId == null)
                return new List<Piece>();

            return context.Pieces.Where(p => p.ArticleID == articleId).ToList();     
                }

        public decimal GetPrix(int pieceId)
        {
            var piece = context.Pieces.Find(pieceId);
            return piece?.PiecePrix ?? 0;
        }
        public int GetQuantiteDispo(int pieceId)
        {
            var piece = context.Pieces.Find(pieceId);
            return piece?.QuantiteDispo ?? 0;
        }
        public void Update(Piece piece)
        {
            var existingPiece = context.Pieces.Find(piece.PieceId);
            if (existingPiece != null)
            {
                existingPiece.PieceName = piece.PieceName;
                existingPiece.PiecePrix = piece.PiecePrix;
                existingPiece.QuantiteDispo = piece.QuantiteDispo;
                existingPiece.ArticleID = piece.ArticleID;

                context.SaveChanges();
            }
        }
    }
}
