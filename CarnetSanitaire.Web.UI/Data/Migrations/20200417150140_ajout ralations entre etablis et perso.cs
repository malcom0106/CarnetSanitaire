using Microsoft.EntityFrameworkCore.Migrations;

namespace CarnetSanitaire.Web.UI.Data.Migrations
{
    public partial class ajoutralationsentreetablisetperso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EtablissementId",
                table: "Personnels",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonnelId",
                table: "Etablissements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_EtablissementId",
                table: "Personnels",
                column: "EtablissementId");

            migrationBuilder.CreateIndex(
                name: "IX_Etablissements_PersonnelId",
                table: "Etablissements",
                column: "PersonnelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Etablissements_Personnels_PersonnelId",
                table: "Etablissements",
                column: "PersonnelId",
                principalTable: "Personnels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnels_Etablissements_EtablissementId",
                table: "Personnels",
                column: "EtablissementId",
                principalTable: "Etablissements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Etablissements_Personnels_PersonnelId",
                table: "Etablissements");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnels_Etablissements_EtablissementId",
                table: "Personnels");

            migrationBuilder.DropIndex(
                name: "IX_Personnels_EtablissementId",
                table: "Personnels");

            migrationBuilder.DropIndex(
                name: "IX_Etablissements_PersonnelId",
                table: "Etablissements");

            migrationBuilder.DropColumn(
                name: "EtablissementId",
                table: "Personnels");

            migrationBuilder.DropColumn(
                name: "PersonnelId",
                table: "Etablissements");
        }
    }
}
