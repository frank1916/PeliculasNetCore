using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.Entidades
{
    public class Genero
    {
        public int id { get; set; }
        [Required]
        [StringLength(40)]
        public string nombre { get; set; }
        public List<PeliculasGeneros> peliculasGeneros { get; set; }
    }
 }
