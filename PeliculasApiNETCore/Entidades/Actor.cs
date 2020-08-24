using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.Entidades
{
    public class Actor
    {
        public int id { get; set; }
        [Required]
        [StringLength(120)]
        public string nombre { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string  foto { get; set; }
        public List<PeliculasActores> peliculasActores { get; set; }

    }
}
