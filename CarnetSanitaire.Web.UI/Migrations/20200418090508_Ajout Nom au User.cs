using Microsoft.EntityFrameworkCore.Migrations;

namespace CarnetSanitaire.Web.UI.Migrations
{
    public partial class AjoutNomauUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nom",
                table: "AspNetUsers");
        }
    }
}
