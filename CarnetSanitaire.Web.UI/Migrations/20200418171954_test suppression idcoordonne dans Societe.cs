using Microsoft.EntityFrameworkCore.Migrations;

namespace CarnetSanitaire.Web.UI.Migrations
{
    public partial class testsuppressionidcoordonnedansSociete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Societes_Coordonnees_CoordonneeId",
                table: "Societes");

            migrationBuilder.AlterColumn<int>(
                name: "CoordonneeId",
                table: "Societes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Societes_Coordonnees_CoordonneeId",
                table: "Societes",
                column: "CoordonneeId",
                principalTable: "Coordonnees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Societes_Coordonnees_CoordonneeId",
                table: "Societes");

            migrationBuilder.AlterColumn<int>(
                name: "CoordonneeId",
                table: "Societes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Societes_Coordonnees_CoordonneeId",
                table: "Societes",
                column: "CoordonneeId",
                principalTable: "Coordonnees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
