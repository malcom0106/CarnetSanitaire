using Microsoft.EntityFrameworkCore.Migrations;

namespace CarnetSanitaire.Web.UI.Data.Migrations
{
    public partial class ajoutpropieteausers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "matricule",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "prenom",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Personnels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(nullable: true),
                    Prenom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Personnels");

            migrationBuilder.DropColumn(
                name: "matricule",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "prenom",
                table: "AspNetUsers");
        }
    }
}
