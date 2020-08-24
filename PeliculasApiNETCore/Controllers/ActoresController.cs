using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApiNETCore.DTOs;
using PeliculasApiNETCore.Entidades;
using PeliculasApiNETCore.Helpers;
using PeliculasApiNETCore.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenardorArchivos almacenardorArchivos;
        private readonly string contenedor = "actores";

        public ActoresController(ApplicationDbContext context, IMapper mapper, IAlmacenardorArchivos almacenardorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenardorArchivos = almacenardorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult <List<ActorDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = this.context.Actores.AsQueryable();
            await HttpContext.InsertarParametrosPaginacion(queryable, paginacionDTO.CantidadRegistrosPorPagina);
            var entidades = await queryable.Paginar(paginacionDTO).ToListAsync();
            return this.mapper.Map<List<ActorDTO>>(entidades);
        }

        [HttpGet("{id:int}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var entidad = await this.context.Actores.FirstOrDefaultAsync(x => x.id == id);

            if (entidad == null)
            {
                return NotFound();
            }
            var dto = this.mapper.Map<ActorDTO>(entidad);

            return dto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var entidad = this.mapper.Map<Actor>(actorCreacionDTO);
            
            if (actorCreacionDTO.foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreacionDTO.foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.foto.FileName);
                    entidad.foto = await this.almacenardorArchivos.GuardarArchivo(contenido, extension, contenedor,
                        actorCreacionDTO.foto.ContentType);
                }
            }
            
            this.context.Add(entidad);
            await this.context.SaveChangesAsync();

            var actorDTO = this.mapper.Map<ActorDTO>(entidad);

            return new CreatedAtRouteResult("obtenerActor", new { id = actorDTO.id }, actorDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            //actualizar solo campos que han sido modificados
            var actorDB = await this.context.Actores.FirstOrDefaultAsync(x => x.id == id);

            if (actorDB == null)
            {
                return NotFound();
            }

            actorDB = mapper.Map(actorCreacionDTO, actorDB);

            if (actorCreacionDTO.foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCreacionDTO.foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.foto.FileName);
                    actorDB.foto = await this.almacenardorArchivos.EditarArchivo(contenido, extension, contenedor,
                        actorDB.foto, actorCreacionDTO.foto.ContentType);
                }
            }
            //this.context.Entry(entidad).State = EntityState.Modified;
            await this.context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch ("{id}")]
        public async Task<ActionResult> Patch (int id, [FromBody] JsonPatchDocument<ActorPatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var entidadDB = await this.context.Actores.FirstOrDefaultAsync(x => x.id == id);

            if (entidadDB == null)
            {
                return NotFound();
            }

            var entidadDTO = this.mapper.Map<ActorPatchDTO>(entidadDB);

            patchDocument.ApplyTo(entidadDTO, ModelState);

            var esValido = TryValidateModel(entidadDTO);

            if (!esValido)
            {
                return BadRequest(ModelState);
            }

            this.mapper.Map(entidadDTO, entidadDB);

            await this.context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await this.context.Actores.AnyAsync(x => x.id == id);
            if (!existe)
            {
                return NotFound(404);
            }
            this.context.Remove(new Actor() { id = id });
            await this.context.SaveChangesAsync();
            return NoContent();
        }
    }
}
