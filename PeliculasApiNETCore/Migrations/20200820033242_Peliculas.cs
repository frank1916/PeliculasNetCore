using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeliculasApiNETCore.Migrations
{
    public partial class Peliculas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Peliculas",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(maxLength: 100, nullable: false),
                    enCines = table.Column<bool>(nullable: false),
                    fechaEstreno = table.Column<DateTime>(nullable: false),
                    poster = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peliculas", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peliculas");
        }
    }
}
