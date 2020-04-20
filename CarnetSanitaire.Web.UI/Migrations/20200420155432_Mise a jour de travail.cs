using Microsoft.EntityFrameworkCore.Migrations;

namespace CarnetSanitaire.Web.UI.Migrations
{
    public partial class Miseajourdetravail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstallationId",
                table: "Travails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Travails_InstallationId",
                table: "Travails",
                column: "InstallationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Travails_Installations_InstallationId",
                table: "Travails",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Travails_Installations_InstallationId",
                table: "Travails");

            migrationBuilder.DropIndex(
                name: "IX_Travails_InstallationId",
                table: "Travails");

            migrationBuilder.DropColumn(
                name: "InstallationId",
                table: "Travails");
        }
    }
}
