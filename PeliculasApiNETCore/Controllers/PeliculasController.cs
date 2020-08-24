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
using System.Security.Policy;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;

namespace PeliculasApiNETCore.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<PeliculasController> logger;
        private readonly IAlmacenardorArchivos almacenardorArchivos;
        private readonly string contenedor = "peliculas";

        public PeliculasController(ApplicationDbContext dbContext, 
            IMapper mapper,
            ILogger<PeliculasController> logger,
            IAlmacenardorArchivos almacenardorArchivos)
        {
            this.context = dbContext;
            this.mapper = mapper;
            this.logger = logger;
            this.almacenardorArchivos = almacenardorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<PeliculasIndexDTO>> Get()
        {
            var top = 5;
            var hoy = DateTime.Today;

            var proximosEstrenos = await this.context.Peliculas
                .Where(x => x.fechaEstreno > hoy)
                .OrderBy(x => x.fechaEstreno)
                .Take(top)
                .ToListAsync();

            var encines = await this.context.Peliculas
                .Where(x => x.enCines)
                .Take(top)
                .ToListAsync();

            var resultado = new PeliculasIndexDTO();
            resultado.futurosEstrenos = mapper.Map<List<PeliculaDTO>>(proximosEstrenos);
            resultado.encines = mapper.Map<List<PeliculaDTO>>(encines);

            return resultado;
        }

        [HttpGet("filtro")]
        public async Task<ActionResult<List<PeliculaDTO>>> Filtrar([FromQuery] FiltroPeliculasDTO filtroPeliculasDTO)
        {
            //EJECUCION DIFERIDA
            var peliculaQueryable = this.context.Peliculas.AsQueryable();
            if (!string.IsNullOrEmpty(filtroPeliculasDTO.titulo))
            {
                peliculaQueryable = peliculaQueryable.Where(x => x.titulo.Contains(filtroPeliculasDTO.titulo));
            }
            if (filtroPeliculasDTO.enCines)
            {
                peliculaQueryable = peliculaQueryable.Where(x => x.enCines);
            }
            if (filtroPeliculasDTO.proximosEstrenos)
            {
                var hoy = DateTime.Today;
                peliculaQueryable = peliculaQueryable.Where(x => x.fechaEstreno > hoy);
            }
            if (filtroPeliculasDTO.generoId != 0)
            {
                peliculaQueryable = peliculaQueryable
                    .Where(x => x.peliculasGeneros.Select(y => y.generoId)
                   .Contains(filtroPeliculasDTO.generoId));
            }

            if (!string.IsNullOrEmpty(filtroPeliculasDTO.campoOrdenar))
            {
                var tipoOrden = filtroPeliculasDTO.ordenAscendente ? "ascending" : "descending";

                try
                {
                peliculaQueryable = peliculaQueryable.OrderBy($"{filtroPeliculasDTO.campoOrdenar} {tipoOrden}");
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex.Message, ex);
                }

            }


            await HttpContext.InsertarParametrosPaginacion(peliculaQueryable,
                filtroPeliculasDTO.cantidadRegistrosPorPagina);

            var peliculas = await peliculaQueryable.Paginar(filtroPeliculasDTO.paginacion).ToListAsync();

            return this.mapper.Map<List<PeliculaDTO>>(peliculas);
        }


        [HttpGet("{id}", Name = "obtenerPelicula")]
        public async Task<ActionResult<PeliculaDetallesDTO>> Get(int id)
        {
            var pelicula = await this.context.Peliculas
                .Include(x => x.peliculasActores).ThenInclude(x => x.actor)
                .Include(x => x.peliculasGeneros).ThenInclude(x => x.genero)
                .FirstOrDefaultAsync(x => x.id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            pelicula.peliculasActores = pelicula.peliculasActores.OrderBy(x => x.orden).ToList();

            return this.mapper.Map<PeliculaDetallesDTO>(pelicula);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm]PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var pelicula = this.mapper.Map<Pelicula>(peliculaCreacionDTO);

            if (peliculaCreacionDTO.poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDTO.poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaCreacionDTO.poster.FileName);
                    pelicula.poster = await this.almacenardorArchivos.GuardarArchivo(contenido, extension, contenedor,
                        peliculaCreacionDTO.poster.ContentType);
                }
            }
            AsignarOrdenActores(pelicula);
            this.context.Add(pelicula);
            await this.context.SaveChangesAsync();
            var peliculaDTO = this.mapper.Map<PeliculaDTO>(pelicula);
            return new CreatedAtRouteResult("obtenerPelicula", new { id = pelicula.id }, peliculaDTO);
        }

        private void AsignarOrdenActores(Pelicula pelicula)
        {
            if (pelicula.peliculasActores != null)
            {
                for (int i = 0; i < pelicula.peliculasActores.Count; i++)
                {
                    pelicula.peliculasActores[i].orden = i;
                }
            }
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, [FromForm] PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var peliculaDB = await this.context.Peliculas
                .Include(x => x.peliculasActores)
                .Include(x => x.peliculasGeneros)
                .FirstOrDefaultAsync(x => x.id == id);

            if (peliculaDB == null)
            {
                return NotFound();
            }

            peliculaDB = mapper.Map(peliculaCreacionDTO, peliculaDB);

            if (peliculaCreacionDTO.poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDTO.poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaCreacionDTO.poster.FileName);
                    peliculaDB.poster = await this.almacenardorArchivos.EditarArchivo(contenido, extension, contenedor,
                        peliculaDB.poster, peliculaCreacionDTO.poster.ContentType);
                }
            }
            //this.context.Entry(entidad).State = EntityState.Modified;
            this.AsignarOrdenActores(peliculaDB);
            await this.context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<PeliculaPatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var entidadDB = await this.context.Peliculas.FirstOrDefaultAsync(x => x.id == id);

            if (entidadDB == null)
            {
                return NotFound();
            }

            var entidadDTO = this.mapper.Map<PeliculaPatchDTO>(entidadDB);

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
            var existe = await this.context.Peliculas.AnyAsync(x => x.id == id);
            if (!existe)
            {
                return NotFound(404);
            }
            this.context.Remove(new Pelicula() { id = id });
            await this.context.SaveChangesAsync();
            return NoContent();
        }



    }
}
