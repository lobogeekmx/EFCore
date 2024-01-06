using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EFCorePeliculas.DTO;
using EFCorePeliculas.Entidades;
using NetTopologySuite.Algorithm.Distance;

namespace EFCorePeliculas.Servicios
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Actor, ActorDTO>();
            CreateMap<Cine, CineDTO>()
                .ForMember(dto => dto.Latitud, ent => ent.MapFrom(prop => prop.Ubicacion.Y))
                .ForMember(dto => dto.Longitud, ent => ent.MapFrom(prop => prop.Ubicacion.X));

            CreateMap<Genero, GeneroDTO>();

            //Sin Project to
            CreateMap<Pelicula, PeliculaDTO>()
                .ForMember(dto => dto.Cines, ent => ent.MapFrom(prop => prop.SalasDeCines.Select(s=> s.Cine)))
                .ForMember(dto => dto.Actores, ent => ent.MapFrom(prop => prop.PeliculasActores.Select(pa => pa.Actor)));


            // Con project to 
            // Es mas directo en el controller, pero toda la configuracion se debe de hacer aqui
            // CreateMap<Pelicula, PeliculaDTO>()
            //     .ForMember(dto => dto.Generos, ent => ent.MapFrom(prop =>
            //         prop.Generos.OrderByDescending(g => g.Nombre)))
            //     .ForMember(dto => dto.Cines, ent => ent.MapFrom(prop => prop.SalasDeCines.Select(s=> s.Cine)))
            //     .ForMember(dto => dto.Actores, ent => 
            //         ent.MapFrom(prop => prop.PeliculasActores
            //             .Where(w=> w.Actor.FechaNacimiento.Value.Year >= 1980)
            //             .Select(pa => pa.Actor)));


            CreateMap<Genero, GeneroDTO>();
        }
    }
}