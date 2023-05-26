using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EspaceCandiat.Data.Migrations
{
    public partial class AddCandidatTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    NiveauEtude = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AnneesExperience = table.Column<int>(type: "int", nullable: false),
                    DernierEmployeur = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Cv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidats", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidats");
        }
    }
}
