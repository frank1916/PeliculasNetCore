using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.DTOs
{
    public class PeliculaPatchDTO
    {
        [Required]
        [StringLength(100)]
        public string titulo { get; set; }
        public bool enCines { get; set; }
        public DateTime fechaEstreno { get; set; }
    }
}
