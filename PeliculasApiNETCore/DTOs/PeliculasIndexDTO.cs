using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.DTOs
{
    public class PeliculasIndexDTO
    {
        public List<PeliculaDTO> futurosEstrenos { get; set; }
        public List<PeliculaDTO> encines { get; set; }
    }
}
