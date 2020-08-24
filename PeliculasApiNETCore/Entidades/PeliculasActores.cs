using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.Entidades
{
    public class PeliculasActores
    {
        public int actorId { get; set; }
        public int peliculaId { get; set; }
        public string nombrePersonaje { get; set; }
        public int orden { get; set; }
        public Actor actor { get; set; }
        public Pelicula pelicula { get; set; }
    }
}
