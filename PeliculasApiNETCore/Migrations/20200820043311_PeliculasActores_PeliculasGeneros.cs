using Microsoft.EntityFrameworkCore.Migrations;

namespace PeliculasApiNETCore.Migrations
{
    public partial class PeliculasActores_PeliculasGeneros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PeliculasActores",
                columns: table => new
                {
                    actorId = table.Column<int>(nullable: false),
                    peliculaId = table.Column<int>(nullable: false),
                    nombrePersonaje = table.Column<string>(nullable: true),
                    orden = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculasActores", x => new { x.actorId, x.peliculaId });
                    table.ForeignKey(
                        name: "FK_PeliculasActores_Actores_actorId",
                        column: x => x.actorId,
                        principalTable: "Actores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculasActores_Peliculas_peliculaId",
                        column: x => x.peliculaId,
                        principalTable: "Peliculas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeliculasGeneros",
                columns: table => new
                {
                    generoId = table.Column<int>(nullable: false),
                    peliculaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculasGeneros", x => new { x.generoId, x.peliculaId });
                    table.ForeignKey(
                        name: "FK_PeliculasGeneros_Generos_generoId",
                        column: x => x.generoId,
                        principalTable: "Generos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculasGeneros_Peliculas_peliculaId",
                        column: x => x.peliculaId,
                        principalTable: "Peliculas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculasActores_peliculaId",
                table: "PeliculasActores",
                column: "peliculaId");

            migrationBuilder.CreateIndex(
                name: "IX_PeliculasGeneros_peliculaId",
                table: "PeliculasGeneros",
                column: "peliculaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeliculasActores");

            migrationBuilder.DropTable(
                name: "PeliculasGeneros");
        }
    }
}
