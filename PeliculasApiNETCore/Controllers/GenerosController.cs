using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApiNETCore.DTOs;
using PeliculasApiNETCore.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenerosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GeneroDTO>>> Get()
        {
            var entidades = await this.context.Generos.ToListAsync();
            var dtos = this.mapper.Map<List<GeneroDTO>>(entidades);
            return dtos;
        }

        [HttpGet("{id:int}", Name = "obtenerGenero")]
        public async Task<ActionResult<GeneroDTO>> Get(int id)
        {
            var entidad = await this.context.Generos.FirstOrDefaultAsync(x => x.id == id);

            if (entidad == null)
            {
                return NotFound();
            }
            var dto = this.mapper.Map<GeneroDTO>(entidad);

            return dto;
        }

        [HttpPost]
        public async Task<ActionResult> Post ([FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            var entidad = this.mapper.Map<Genero>(generoCreacionDTO);
            this.context.Add(entidad);
            await this.context.SaveChangesAsync();

            var generoDTO = this.mapper.Map<GeneroDTO>(entidad);

            return new CreatedAtRouteResult("obtenerGenero", new { id = generoDTO.id }, generoDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            var entidad = this.mapper.Map<Genero>(generoCreacionDTO);
            entidad.id = id;
            this.context.Entry(entidad).State = EntityState.Modified;
            await this.context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete ("{id}")]
        public async Task<ActionResult> Delete (int id)
        {
            var existe = await this.context.Generos.AnyAsync(x => x.id == id);
            if (!existe)
            {
                return NotFound(404);
            }
            this.context.Remove(new Genero() { id = id});
            await this.context.SaveChangesAsync();
            return NoContent();
        }

    }
}
