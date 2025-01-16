﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniPorjet.Context;

#nullable disable

namespace MiniPorjet.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250115160808_test2")]
    partial class test2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MiniPorjet.Models.Article", b =>
                {
                    b.Property<int>("ArticleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArticleID"));

                    b.Property<string>("ArticleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ArticleType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArticleID");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            ArticleID = 1,
                            ArticleName = "Article 1",
                            ArticleType = "Type A"
                        },
                        new
                        {
                            ArticleID = 2,
                            ArticleName = "Article 2",
                            ArticleType = "Type B"
                        },
                        new
                        {
                            ArticleID = 3,
                            ArticleName = "Article 3",
                            ArticleType = "Type C"
                        });
                });

            modelBuilder.Entity("MiniPorjet.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientId"));

                    b.Property<string>("ClientAdresse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientTelephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            ClientId = 1,
                            ClientAdresse = "Address 1",
                            ClientName = "Client 1",
                            ClientTelephone = "123456789"
                        },
                        new
                        {
                            ClientId = 2,
                            ClientAdresse = "Address 2",
                            ClientName = "Client 2",
                            ClientTelephone = "987654321"
                        });
                });

            modelBuilder.Entity("MiniPorjet.Models.ClientArticle", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAchat")
                        .HasColumnType("datetime2");

                    b.HasKey("ClientId", "ArticleId");

                    b.HasIndex("ArticleId");

                    b.ToTable("ClientArticles");

                    b.HasData(
                        new
                        {
                            ClientId = 1,
                            ArticleId = 1,
                            DateAchat = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ClientId = 1,
                            ArticleId = 2,
                            DateAchat = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ClientId = 2,
                            ArticleId = 3,
                            DateAchat = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("MiniPorjet.Models.Intervention", b =>
                {
                    b.Property<int>("InterventionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InterventionId"));

                    b.Property<DateTime>("InterventionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReclamationId")
                        .HasColumnType("int");

                    b.Property<int>("prixIntervention")
                        .HasColumnType("int");

                    b.HasKey("InterventionId");

                    b.HasIndex("ReclamationId");

                    b.ToTable("Interventions");
                });

            modelBuilder.Entity("MiniPorjet.Models.Piece", b =>
                {
                    b.Property<int>("PieceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PieceId"));

                    b.Property<int>("ArticleID")
                        .HasColumnType("int");

                    b.Property<string>("PieceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PiecePrix")
                        .HasColumnType("int");

                    b.Property<int>("QuantiteDispo")
                        .HasColumnType("int");

                    b.HasKey("PieceId");

                    b.HasIndex("ArticleID");

                    b.ToTable("Pieces");

                    b.HasData(
                        new
                        {
                            PieceId = 1,
                            ArticleID = 1,
                            PieceName = "Piece 1",
                            PiecePrix = 123,
                            QuantiteDispo = 5
                        },
                        new
                        {
                            PieceId = 2,
                            ArticleID = 1,
                            PieceName = "Piece 2",
                            PiecePrix = 70,
                            QuantiteDispo = 7
                        },
                        new
                        {
                            PieceId = 3,
                            ArticleID = 1,
                            PieceName = "Piece 3",
                            PiecePrix = 60,
                            QuantiteDispo = 1
                        });
                });

            modelBuilder.Entity("MiniPorjet.Models.Reclamation", b =>
                {
                    b.Property<int>("ReclamationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReclamationId"));

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("PieceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReclamationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReclamationDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReclamationStatut")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReclamationId");

                    b.HasIndex("ArticleId");

                    b.HasIndex("ClientId");

                    b.HasIndex("PieceId");

                    b.ToTable("Reclamations");

                    b.HasData(
                        new
                        {
                            ReclamationId = 1,
                            ArticleId = 1,
                            ClientId = 1,
                            PieceId = 2,
                            ReclamationDate = new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReclamationDescription = "Issue with Article 1",
                            ReclamationStatut = "Nouvelle"
                        },
                        new
                        {
                            ReclamationId = 2,
                            ArticleId = 3,
                            ClientId = 2,
                            PieceId = 3,
                            ReclamationDate = new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReclamationDescription = "Issue with Article 2",
                            ReclamationStatut = "Résolue"
                        });
                });

            modelBuilder.Entity("MiniPorjet.Models.ClientArticle", b =>
                {
                    b.HasOne("MiniPorjet.Models.Article", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniPorjet.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("MiniPorjet.Models.Intervention", b =>
                {
                    b.HasOne("MiniPorjet.Models.Reclamation", "Reclamation")
                        .WithMany()
                        .HasForeignKey("ReclamationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reclamation");
                });

            modelBuilder.Entity("MiniPorjet.Models.Piece", b =>
                {
                    b.HasOne("MiniPorjet.Models.Article", "Article")
                        .WithMany("Pieces")
                        .HasForeignKey("ArticleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");
                });

            modelBuilder.Entity("MiniPorjet.Models.Reclamation", b =>
                {
                    b.HasOne("MiniPorjet.Models.Article", "Article")
                        .WithMany("Reclamations")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniPorjet.Models.Client", "Client")
                        .WithMany("Reclamations")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniPorjet.Models.Piece", "Piece")
                        .WithMany("Reclamations")
                        .HasForeignKey("PieceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Client");

                    b.Navigation("Piece");
                });

            modelBuilder.Entity("MiniPorjet.Models.Article", b =>
                {
                    b.Navigation("Pieces");

                    b.Navigation("Reclamations");
                });

            modelBuilder.Entity("MiniPorjet.Models.Client", b =>
                {
                    b.Navigation("Reclamations");
                });

            modelBuilder.Entity("MiniPorjet.Models.Piece", b =>
                {
                    b.Navigation("Reclamations");
                });
#pragma warning restore 612, 618
        }
    }
}
