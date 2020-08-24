using Microsoft.AspNetCore.Http;
using PeliculasApiNETCore.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.DTOs
{
    public class ActorCreacionDTO: ActorPatchDTO
    {
        [PesoArchivoValidacion(pesoMaximoMegaBytes:4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen )]
        public IFormFile  foto { get; set; }
    }
}
