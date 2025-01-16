using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniPorjet.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PieceId",
                table: "Reclamations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Reclamations",
                keyColumn: "ReclamationId",
                keyValue: 1,
                column: "PieceId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Reclamations",
                keyColumn: "ReclamationId",
                keyValue: 2,
                columns: new[] { "ArticleId", "PieceId" },
                values: new object[] { 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Reclamations_PieceId",
                table: "Reclamations",
                column: "PieceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reclamations_Pieces_PieceId",
                table: "Reclamations",
                column: "PieceId",
                principalTable: "Pieces",
                principalColumn: "PieceId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reclamations_Pieces_PieceId",
                table: "Reclamations");

            migrationBuilder.DropIndex(
                name: "IX_Reclamations_PieceId",
                table: "Reclamations");

            migrationBuilder.DropColumn(
                name: "PieceId",
                table: "Reclamations");

            migrationBuilder.UpdateData(
                table: "Reclamations",
                keyColumn: "ReclamationId",
                keyValue: 2,
                column: "ArticleId",
                value: 3);
        }
    }
}
