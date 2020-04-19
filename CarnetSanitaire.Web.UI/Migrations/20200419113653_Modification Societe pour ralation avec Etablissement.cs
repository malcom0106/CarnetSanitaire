using Microsoft.EntityFrameworkCore.Migrations;

namespace CarnetSanitaire.Web.UI.Migrations
{
    public partial class ModificationSocietepourralationavecEtablissement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Societes_Etablissements_EtablissementId",
                table: "Societes");

            migrationBuilder.AlterColumn<int>(
                name: "EtablissementId",
                table: "Societes",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Societes_Etablissements_EtablissementId",
                table: "Societes",
                column: "EtablissementId",
                principalTable: "Etablissements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Societes_Etablissements_EtablissementId",
                table: "Societes");

            migrationBuilder.AlterColumn<int>(
                name: "EtablissementId",
                table: "Societes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Societes_Etablissements_EtablissementId",
                table: "Societes",
                column: "EtablissementId",
                principalTable: "Etablissements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
