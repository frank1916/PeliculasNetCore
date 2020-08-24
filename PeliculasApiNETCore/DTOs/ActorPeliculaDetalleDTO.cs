using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.DTOs
{
    public class ActorPeliculaDetalleDTO
    {
        public int actorId { get; set; }
        public string personaje { get; set; }
        public string nombrePersona { get; set; }
    }
}
