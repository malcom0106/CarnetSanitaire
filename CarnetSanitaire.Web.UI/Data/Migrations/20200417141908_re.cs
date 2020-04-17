using Microsoft.EntityFrameworkCore.Migrations;

namespace CarnetSanitaire.Web.UI.Data.Migrations
{
    public partial class re : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordonnees_Etablissements_EtablissementId",
                table: "Coordonnees");

            migrationBuilder.DropIndex(
                name: "IX_Coordonnees_EtablissementId",
                table: "Coordonnees");

            migrationBuilder.DropColumn(
                name: "EtablissementId",
                table: "Coordonnees");

            migrationBuilder.AddColumn<int>(
                name: "IdCoordonnee",
                table: "Etablissements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Etablissements_IdCoordonnee",
                table: "Etablissements",
                column: "IdCoordonnee");

            migrationBuilder.AddForeignKey(
                name: "FK_Etablissements_Coordonnees_IdCoordonnee",
                table: "Etablissements",
                column: "IdCoordonnee",
                principalTable: "Coordonnees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Etablissements_Coordonnees_IdCoordonnee",
                table: "Etablissements");

            migrationBuilder.DropIndex(
                name: "IX_Etablissements_IdCoordonnee",
                table: "Etablissements");

            migrationBuilder.DropColumn(
                name: "IdCoordonnee",
                table: "Etablissements");

            migrationBuilder.AddColumn<int>(
                name: "EtablissementId",
                table: "Coordonnees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Coordonnees_EtablissementId",
                table: "Coordonnees",
                column: "EtablissementId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Coordonnees_Etablissements_EtablissementId",
                table: "Coordonnees",
                column: "EtablissementId",
                principalTable: "Etablissements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
