using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeliculasApiNETCore.Helpers;
using PeliculasApiNETCore.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.DTOs
{
    public class PeliculaCreacionDTO: PeliculaPatchDTO
    {
        [PesoArchivoValidacion(pesoMaximoMegaBytes: 4)]
        [TipoArchivoValidacion(GrupoTipoArchivo.Imagen)]
        public IFormFile poster { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> generosIds { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<ActorPeliculasCreacionDTO>>))]
        public List<ActorPeliculasCreacionDTO> actores { get; set; } 

    }
}
