using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.DTOs
{
    public class PeliculaDTO
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public bool enCines { get; set; }
        public DateTime fechaEstreno { get; set; }
        public string poster { get; set; }
    }
}
