using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.Entidades
{
    public class PeliculasGeneros
    {
        public int generoId { get; set; }
        public int peliculaId { get; set; }
        public Genero genero { get; set; }
        public Pelicula pelicula { get; set; }
         
    }
}
