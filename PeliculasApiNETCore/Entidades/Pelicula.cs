using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.Entidades
{
    public class Pelicula
    {
        public int id { get; set; }
        [Required]
        [StringLength(100)]
        public string titulo { get; set; }
        public bool enCines { get; set; }
        public DateTime fechaEstreno { get; set; }
        public string poster { get; set; }

        public List<PeliculasActores> peliculasActores { get; set; }
        public List<PeliculasGeneros> peliculasGeneros { get; set; }

    }
}
