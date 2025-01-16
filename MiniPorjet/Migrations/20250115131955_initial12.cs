using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniPorjet.Migrations
{
    /// <inheritdoc />
    public partial class initial12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Ajout de la colonne PieceId
            migrationBuilder.AddColumn<int>(
                name: "PieceId",
                table: "Reclamations",
                nullable: false,
                defaultValue: 0);

            // Autres mises à jour de données ou tables...

            // Ajout de la contrainte de clé étrangère sans cascade
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

        }
    }
}
