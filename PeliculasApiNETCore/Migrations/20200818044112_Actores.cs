using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeliculasApiNETCore.Migrations
{
    public partial class Actores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actores",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(maxLength: 120, nullable: false),
                    fechaNacimiento = table.Column<DateTime>(nullable: false),
                    foto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actores", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actores");
        }
    }
}
