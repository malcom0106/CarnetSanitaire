using Microsoft.EntityFrameworkCore.Migrations;

namespace CarnetSanitaire.Web.UI.Data.Migrations
{
    public partial class maj170420203 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "prenom",
                table: "AspNetUsers",
                newName: "Prenom");

            migrationBuilder.RenameColumn(
                name: "matricule",
                table: "AspNetUsers",
                newName: "Matricule");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prenom",
                table: "AspNetUsers",
                newName: "prenom");

            migrationBuilder.RenameColumn(
                name: "Matricule",
                table: "AspNetUsers",
                newName: "matricule");
        }
    }
}
