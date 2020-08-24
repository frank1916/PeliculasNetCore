using AutoMapper;
using PeliculasApiNETCore.DTOs;
using PeliculasApiNETCore.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>()
                .ForMember(x => x.foto, options => options.Ignore());
            CreateMap<ActorPatchDTO, Actor>().ReverseMap();

            CreateMap<Pelicula, PeliculaDTO>().ReverseMap();
            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(x => x.poster, options => options.Ignore())
                .ForMember(x => x.peliculasGeneros, options => options.MapFrom(mapPeliculasGeneros))
                .ForMember(x => x.peliculasActores, options => options.MapFrom(mapPeliculasAutores));

            CreateMap<Pelicula, PeliculaDetallesDTO>()
                .ForMember(x => x.generos, options => options.MapFrom(mapPeliculasGeneros))
                .ForMember(x => x.actores, options => options.MapFrom(mapPeliculasActores));


            CreateMap<PeliculaPatchDTO, Pelicula>().ReverseMap();
        }

        private List<ActorPeliculaDetalleDTO> mapPeliculasActores(Pelicula pelicula, PeliculaDetallesDTO peliculaDetallesDTO)
        {
            var resultado = new List<ActorPeliculaDetalleDTO>();
            if (pelicula.peliculasActores == null)
            {
                return resultado;
            }
            foreach(var actorPelicula in pelicula.peliculasActores)
            {
                resultado.Add(new ActorPeliculaDetalleDTO
                {
                    actorId = actorPelicula.actorId,
                    personaje = actorPelicula.nombrePersonaje,
                    nombrePersona = actorPelicula.actor.nombre
                });
            }
            return resultado;
        }

        private  List<GeneroDTO> mapPeliculasGeneros(Pelicula pelicula, PeliculaDetallesDTO peliculaDetallesDTO)
        {
            var resultado = new List<GeneroDTO>();
            if (pelicula.peliculasGeneros == null)
            {
                return resultado;
            }
            foreach (var generoPelicula in pelicula.peliculasGeneros)
            {
                resultado.Add(new GeneroDTO() { id = generoPelicula.generoId, nombre = generoPelicula.genero.nombre });
            }
            return resultado;
        }

        private List<PeliculasActores> mapPeliculasAutores(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculasActores>();
            if (peliculaCreacionDTO.actores == null)
            {
                return resultado;
            }
            foreach (var actor in peliculaCreacionDTO.actores)
            {
                resultado.Add(new PeliculasActores() {  actorId = actor.actorId, nombrePersonaje = actor.personaje });
            }
            return resultado;
        }

        private List<PeliculasGeneros> mapPeliculasGeneros(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculasGeneros>();
            if (peliculaCreacionDTO.generosIds == null)
            {
                return resultado;
            }
            foreach (var id in peliculaCreacionDTO.generosIds)
            {
                resultado.Add(new PeliculasGeneros() { generoId = id }); 
            }
            return resultado;
        }
    }
}
