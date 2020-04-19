using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarnetSanitaire.Web.UI.Migrations
{
    public partial class ModificationPersonnel19042020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogErreurs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageErreur = table.Column<string>(nullable: true),
                    DateErreur = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogErreurs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogErreurs");
        }
    }
}
