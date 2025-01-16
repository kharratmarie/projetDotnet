using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniPorjet.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "ArticleID", "ArticleName", "ArticleType" },
                values: new object[,]
                {
                    { 1, "Article 1", "Type A" },
                    { 2, "Article 2", "Type B" },
                    { 3, "Article 3", "Type C" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "ClientAdresse", "ClientName", "ClientTelephone" },
                values: new object[,]
                {
                    { 1, "Address 1", "Client 1", "123456789" },
                    { 2, "Address 2", "Client 2", "987654321" }
                });

            migrationBuilder.InsertData(
                table: "ClientArticles",
                columns: new[] { "ArticleId", "ClientId", "DateAchat" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Pieces",
                columns: new[] { "PieceId", "ArticleID", "PieceName", "PiecePrix", "QuantiteDispo" },
                values: new object[,]
                {
                    { 1, 1, "Piece 1", 123, 5 },
                    { 2, 1, "Piece 2", 70, 7 },
                    { 3, 1, "Piece 3", 60, 1 }
                });

            migrationBuilder.InsertData(
                table: "Reclamations",
                columns: new[] { "ReclamationId", "ArticleId", "ClientId", "ReclamationDate", "ReclamationDescription", "ReclamationStatut" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Issue with Article 1", "Nouvelle" },
                    { 2, 3, 2, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Issue with Article 2", "Résolue" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClientArticles",
                keyColumns: new[] { "ArticleId", "ClientId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ClientArticles",
                keyColumns: new[] { "ArticleId", "ClientId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ClientArticles",
                keyColumns: new[] { "ArticleId", "ClientId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "Pieces",
                keyColumn: "PieceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pieces",
                keyColumn: "PieceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pieces",
                keyColumn: "PieceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reclamations",
                keyColumn: "ReclamationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reclamations",
                keyColumn: "ReclamationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 2);
        }
    }
}
