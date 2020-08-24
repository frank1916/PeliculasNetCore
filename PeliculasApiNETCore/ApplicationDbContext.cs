using Microsoft.EntityFrameworkCore;
using PeliculasApiNETCore.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeliculasActores>()
                .HasKey(x => new {x.actorId, x.peliculaId });

            modelBuilder.Entity<PeliculasGeneros>()
                .HasKey(x => new {  x.generoId, x.peliculaId });

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {

            //var rolAdminid = "9aae0b6d-d50c-4d0a-9b90-2a6873e3845d";
            //var usuarioAdminid = "5673b8cf-12de-44f6-92ad-fae4a77932ad";

            //var rolAdmin = new identityRole()
            //{
            //    id = rolAdminid,
            //    Name = "Admin",
            //    NormalizedName = "Admin"
            //};

            //var passwordHasher = new PasswordHasher<identityUser>();

            //var username = "felipe@hotmail.com";

            //var usuarioAdmin = new identityUser()
            //{
            //    id = usuarioAdminid,
            //    UserName = username,
            //    NormalizedUserName = username,
            //    Email = username,
            //    NormalizedEmail = username,
            //    PasswordHash = passwordHasher.HashPassword(null, "Aa123456!")
            //};

            //modelBuilder.Entity<identityUser>()
            //    .HasData(usuarioAdmin);

            //modelBuilder.Entity<identityRole>()
            //    .HasData(rolAdmin);

            //modelBuilder.Entity<identityUserClaim<string>>()
            //    .HasData(new identityUserClaim<string>()
            //    {
            //        id = 1,
            //        ClaimType = ClaimTypes.Role,
            //        Userid = usuarioAdminid,
            //        ClaimValue = "Admin"
            //    });

            //var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            //modelBuilder.Entity<SalaDeCine>()
            //   .HasData(new List<SalaDeCine>
            //   {
            //        //new SalaDeCine{id = 1, nombre = "Agora", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-69.9388777, 18.4839233))},
            //        new SalaDeCine{id = 4, nombre = "Sambil", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-69.9118804, 18.4826214))},
            //        new SalaDeCine{id = 5, nombre = "Megacentro", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-69.856427, 18.506934))},
            //        new SalaDeCine{id = 6, nombre = "Village East Cinema", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-73.986227, 40.730898))}
            //   });

            var aventura = new Genero() { id = 14, nombre = "Aventura" };
            var animation = new Genero() { id = 15, nombre = "Animación" };
            var suspenso = new Genero() { id = 16, nombre = "Suspenso" };
            var romance = new Genero() { id = 17, nombre = "Romance" };

            modelBuilder.Entity<Genero>()
                .HasData(new List<Genero>
                {
                    aventura, animation, suspenso, romance
                });

            var jimCarrey = new Actor() { id = 15, nombre = "Jim Carrey", fechaNacimiento = new DateTime(1962, 01, 17) };
            var robertDowney = new Actor() { id = 16, nombre = "Robert Downey Jr.", fechaNacimiento = new DateTime(1965, 4, 4) };
            var chrisEvans = new Actor() { id = 17, nombre = "Chris Evans", fechaNacimiento = new DateTime(1981, 06, 13) };

            modelBuilder.Entity<Actor>()
                .HasData(new List<Actor>
                {
                    jimCarrey, robertDowney, chrisEvans
                });

            var endgame = new Pelicula()
            {
                id = 12,
                titulo = "Avengers: Endgame",
                enCines = true,
                fechaEstreno = new DateTime(2019, 04, 26)
            };

            var iw = new Pelicula()
            {
                id = 13,
                titulo = "Avengers: Infinity Wars",
                enCines = false,
                fechaEstreno = new DateTime(2019, 04, 26)
            };

            var sonic = new Pelicula()
            {
                id = 14,
                titulo = "Sonic the Hedgehog",
                enCines = false,
                fechaEstreno = new DateTime(2020, 02, 28)
            };
            var emma = new Pelicula()
            {
                id = 15,
                titulo = "Emma",
                enCines = false,
                fechaEstreno = new DateTime(2020, 02, 21)
            };
            var wonderwoman = new Pelicula()
            {
                id = 16,
                titulo = "Wonder Woman 1984",
                enCines = false,
                fechaEstreno = new DateTime(2020, 08, 14)
            };

            modelBuilder.Entity<Pelicula>()
                .HasData(new List<Pelicula>
                {
                    endgame, iw, sonic, emma, wonderwoman
                });

            modelBuilder.Entity<PeliculasGeneros>().HasData(
                new List<PeliculasGeneros>()
                {
                    new PeliculasGeneros(){peliculaId = endgame.id, generoId = suspenso.id},
                    new PeliculasGeneros(){peliculaId = endgame.id, generoId = aventura.id},
                    new PeliculasGeneros(){peliculaId = iw.id, generoId = suspenso.id},
                    new PeliculasGeneros(){peliculaId = iw.id, generoId = aventura.id},
                    new PeliculasGeneros(){peliculaId = sonic.id, generoId = aventura.id},
                    new PeliculasGeneros(){peliculaId = emma.id, generoId = suspenso.id},
                    new PeliculasGeneros(){peliculaId = emma.id, generoId = romance.id},
                    new PeliculasGeneros(){peliculaId = wonderwoman.id, generoId = suspenso.id},
                    new PeliculasGeneros(){peliculaId = wonderwoman.id, generoId = aventura.id},
                });

            modelBuilder.Entity<PeliculasActores>().HasData(
                new List<PeliculasActores>()
                {
                    new PeliculasActores(){peliculaId = endgame.id, actorId = robertDowney.id, nombrePersonaje = "Tony Stark", orden = 1},
                    new PeliculasActores(){peliculaId = endgame.id, actorId = chrisEvans.id, nombrePersonaje = "Steve Rogers", orden = 2},
                    new PeliculasActores(){peliculaId = iw.id, actorId = robertDowney.id, nombrePersonaje = "Tony Stark", orden = 1},
                    new PeliculasActores(){peliculaId = iw.id, actorId = chrisEvans.id, nombrePersonaje = "Steve Rogers", orden = 2},
                    new PeliculasActores(){peliculaId = sonic.id, actorId = jimCarrey.id, nombrePersonaje = "Dr. Ivo Robotnik", orden = 1}
                });
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; } 
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<PeliculasActores> PeliculasActores { get; set; }
        public DbSet<PeliculasGeneros> PeliculasGeneros { get; set; }

    }
}
