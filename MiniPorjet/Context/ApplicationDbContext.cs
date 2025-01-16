using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniPorjet.Models;

namespace MiniPorjet.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientArticle> ClientArticles { get; set; }
        public DbSet<Piece> Pieces { get; set; }
        public DbSet<Reclamation> Reclamations { get; set; }
        public DbSet<Intervention> Interventions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Définir une clé composite pour la table d'association ClientArticle
            modelBuilder.Entity<ClientArticle>()
                .HasKey(ca => new { ca.ClientId, ca.ArticleId });


            // Ajouter des données statiques pour la table Articles
            modelBuilder.Entity<Article>().HasData(
                new Article { ArticleID = 1, ArticleName = "Article 1", ArticleType = "Type A" },
                new Article { ArticleID = 2, ArticleName = "Article 2", ArticleType = "Type B" },
                new Article { ArticleID = 3, ArticleName = "Article 3", ArticleType = "Type C" }
            );

            // Ajouter des données statiques pour la table Clients
            modelBuilder.Entity<Client>().HasData(
                new Client { ClientId = 1, ClientName = "Client 1", ClientAdresse = "Address 1", ClientTelephone = "123456789" },
                new Client { ClientId = 2, ClientName = "Client 2", ClientAdresse = "Address 2", ClientTelephone = "987654321" }
            );

            // Ajouter des données statiques pour la table ClientArticles
            modelBuilder.Entity<ClientArticle>().HasData(
                new ClientArticle { ClientId = 1, ArticleId = 1, DateAchat = new DateTime(2025, 1, 1) },
                new ClientArticle { ClientId = 1, ArticleId = 2, DateAchat = new DateTime(2025, 1, 1) },
                new ClientArticle { ClientId = 2, ArticleId = 3, DateAchat = new DateTime(2025, 1, 1) }
            );

            // Ajouter des données statiques pour la table Pieces
            modelBuilder.Entity<Piece>().HasData(
                new Piece { PieceId = 1, PieceName = "Piece 1", PiecePrix= 123,QuantiteDispo=5 , ArticleID = 1 },
                new Piece { PieceId = 2, PieceName = "Piece 2", PiecePrix = 70, QuantiteDispo = 7, ArticleID = 1 },
                new Piece { PieceId = 3, PieceName = "Piece 3", PiecePrix = 60, QuantiteDispo = 1, ArticleID = 1 }
            );

            // Ajouter des données statiques pour la table Reclamations
            modelBuilder.Entity<Reclamation>().HasData(
                new Reclamation { ReclamationId = 1, ReclamationDescription = "Issue with Article 1", ReclamationStatut = "Nouvelle",ReclamationDate = new DateTime(2025, 1, 15),ClientId = 1  ,ArticleId = 1, PieceId = 2 },
                new Reclamation { ReclamationId = 2, ReclamationDescription = "Issue with Article 2",ReclamationStatut = "Résolue", ReclamationDate = new DateTime(2025, 2, 15), ClientId = 2 ,ArticleId = 3, PieceId = 3 }
            );


            base.OnModelCreating(modelBuilder);
        }


    }
}
