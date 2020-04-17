using Microsoft.EntityFrameworkCore.Migrations;

namespace CarnetSanitaire.Web.UI.Data.Migrations
{
    public partial class ajoutdesdataannotationssurlespremierestables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "Personnels",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Personnels",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoordonneeId",
                table: "Etablissements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Ville",
                table: "Coordonnees",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telephone",
                table: "Coordonnees",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Coordonnees",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodePostal",
                table: "Coordonnees",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Adresse",
                table: "Coordonnees",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EtablissementId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Etablissements_CoordonneeId",
                table: "Etablissements",
                column: "CoordonneeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EtablissementId",
                table: "AspNetUsers",
                column: "EtablissementId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Etablissements_EtablissementId",
                table: "AspNetUsers",
                column: "EtablissementId",
                principalTable: "Etablissements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Etablissements_Coordonnees_CoordonneeId",
                table: "Etablissements",
                column: "CoordonneeId",
                principalTable: "Coordonnees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Etablissements_EtablissementId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Etablissements_Coordonnees_CoordonneeId",
                table: "Etablissements");

            migrationBuilder.DropIndex(
                name: "IX_Etablissements_CoordonneeId",
                table: "Etablissements");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EtablissementId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CoordonneeId",
                table: "Etablissements");

            migrationBuilder.DropColumn(
                name: "EtablissementId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "Personnels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Personnels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "IdCoordonnee",
                table: "Etablissements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Ville",
                table: "Coordonnees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Telephone",
                table: "Coordonnees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Coordonnees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "CodePostal",
                table: "Coordonnees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Adresse",
                table: "Coordonnees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

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
    }
}
