﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.DTOs
{
    public class ActorPatchDTO
    {
        [Required]
        [StringLength(120)]
        public string nombre { get; set; }
        public DateTime fechaNacimiento { get; set; }
    }
}
