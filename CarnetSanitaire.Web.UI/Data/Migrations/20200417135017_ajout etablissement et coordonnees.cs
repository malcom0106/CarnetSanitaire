using Microsoft.EntityFrameworkCore.Migrations;

namespace CarnetSanitaire.Web.UI.Data.Migrations
{
    public partial class ajoutetablissementetcoordonnees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coordonnees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresse = table.Column<string>(nullable: true),
                    SubAdresse = table.Column<string>(nullable: true),
                    CodePostal = table.Column<string>(nullable: true),
                    Ville = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordonnees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Etablissements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<int>(nullable: false),
                    Capacite = table.Column<int>(nullable: false),
                    IdCoordonnee = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etablissements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Etablissements_Coordonnees_IdCoordonnee",
                        column: x => x.IdCoordonnee,
                        principalTable: "Coordonnees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Etablissements_IdCoordonnee",
                table: "Etablissements",
                column: "IdCoordonnee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Etablissements");

            migrationBuilder.DropTable(
                name: "Coordonnees");
        }
    }
}
