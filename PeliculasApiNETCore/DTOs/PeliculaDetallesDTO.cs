using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.DTOs
{
    public class PeliculaDetallesDTO: PeliculaDTO
    {
        public List<GeneroDTO> generos { get; set; }
        public List<ActorPeliculaDetalleDTO> actores { get; set; }
    }
}
