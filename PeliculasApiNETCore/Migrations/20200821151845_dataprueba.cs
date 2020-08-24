using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PeliculasApiNETCore.Migrations
{
    public partial class dataprueba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Actores",
                columns: new[] { "id", "fechaNacimiento", "foto", "nombre" },
                values: new object[,]
                {
                    { 15, new DateTime(1962, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jim Carrey" },
                    { 16, new DateTime(1965, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Robert Downey Jr." },
                    { 17, new DateTime(1981, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Chris Evans" }
                });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "id", "nombre" },
                values: new object[,]
                {
                    { 14, "Aventura" },
                    { 15, "Animación" },
                    { 16, "Suspenso" },
                    { 17, "Romance" }
                });

            migrationBuilder.InsertData(
                table: "Peliculas",
                columns: new[] { "id", "enCines", "fechaEstreno", "poster", "titulo" },
                values: new object[,]
                {
                    { 12, true, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Avengers: Endgame" },
                    { 13, false, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Avengers: Infinity Wars" },
                    { 14, false, new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sonic the Hedgehog" },
                    { 15, false, new DateTime(2020, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Emma" },
                    { 16, false, new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Wonder Woman 1984" }
                });

            migrationBuilder.InsertData(
                table: "PeliculasActores",
                columns: new[] { "actorId", "peliculaId", "nombrePersonaje", "orden" },
                values: new object[,]
                {
                    { 16, 12, "Tony Stark", 1 },
                    { 17, 12, "Steve Rogers", 2 },
                    { 16, 13, "Tony Stark", 1 },
                    { 17, 13, "Steve Rogers", 2 },
                    { 15, 14, "Dr. Ivo Robotnik", 1 }
                });

            migrationBuilder.InsertData(
                table: "PeliculasGeneros",
                columns: new[] { "generoId", "peliculaId" },
                values: new object[,]
                {
                    { 16, 12 },
                    { 14, 12 },
                    { 16, 13 },
                    { 14, 13 },
                    { 14, 14 },
                    { 16, 15 },
                    { 17, 15 },
                    { 16, 16 },
                    { 14, 16 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "PeliculasActores",
                keyColumns: new[] { "actorId", "peliculaId" },
                keyValues: new object[] { 15, 14 });

            migrationBuilder.DeleteData(
                table: "PeliculasActores",
                keyColumns: new[] { "actorId", "peliculaId" },
                keyValues: new object[] { 16, 12 });

            migrationBuilder.DeleteData(
                table: "PeliculasActores",
                keyColumns: new[] { "actorId", "peliculaId" },
                keyValues: new object[] { 16, 13 });

            migrationBuilder.DeleteData(
                table: "PeliculasActores",
                keyColumns: new[] { "actorId", "peliculaId" },
                keyValues: new object[] { 17, 12 });

            migrationBuilder.DeleteData(
                table: "PeliculasActores",
                keyColumns: new[] { "actorId", "peliculaId" },
                keyValues: new object[] { 17, 13 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "generoId", "peliculaId" },
                keyValues: new object[] { 14, 12 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "generoId", "peliculaId" },
                keyValues: new object[] { 14, 13 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "generoId", "peliculaId" },
                keyValues: new object[] { 14, 14 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "generoId", "peliculaId" },
                keyValues: new object[] { 14, 16 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "generoId", "peliculaId" },
                keyValues: new object[] { 16, 12 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "generoId", "peliculaId" },
                keyValues: new object[] { 16, 13 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "generoId", "peliculaId" },
                keyValues: new object[] { 16, 15 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "generoId", "peliculaId" },
                keyValues: new object[] { 16, 16 });

            migrationBuilder.DeleteData(
                table: "PeliculasGeneros",
                keyColumns: new[] { "generoId", "peliculaId" },
                keyValues: new object[] { 17, 15 });

            migrationBuilder.DeleteData(
                table: "Actores",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Actores",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Actores",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "id",
                keyValue: 16);
        }
    }
}
