using Microsoft.EntityFrameworkCore.Migrations;

namespace CarnetSanitaire.Web.UI.Migrations
{
    public partial class relationNNInstallationetmateriau : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materiaus_Installations_InstallationId",
                table: "Materiaus");

            migrationBuilder.DropIndex(
                name: "IX_Materiaus_InstallationId",
                table: "Materiaus");

            migrationBuilder.DropColumn(
                name: "InstallationId",
                table: "Materiaus");

            migrationBuilder.CreateTable(
                name: "InstallationMateriaus",
                columns: table => new
                {
                    InstallationId = table.Column<int>(nullable: false),
                    MateriauId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallationMateriaus", x => new { x.InstallationId, x.MateriauId });
                    table.ForeignKey(
                        name: "FK_InstallationMateriaus_Installations_InstallationId",
                        column: x => x.InstallationId,
                        principalTable: "Installations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstallationMateriaus_Materiaus_MateriauId",
                        column: x => x.MateriauId,
                        principalTable: "Materiaus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstallationMateriaus_MateriauId",
                table: "InstallationMateriaus",
                column: "MateriauId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstallationMateriaus");

            migrationBuilder.AddColumn<int>(
                name: "InstallationId",
                table: "Materiaus",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materiaus_InstallationId",
                table: "Materiaus",
                column: "InstallationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materiaus_Installations_InstallationId",
                table: "Materiaus",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
