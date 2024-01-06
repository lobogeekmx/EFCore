using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCorePeliculas.DTO;
using EFCorePeliculas.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PeliculasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PeliculaDTO>> Get(int id)
        {
            var pelicula = await context.Peliculas
                    .Include(p => p.Generos.OrderByDescending(g => g.Nombre))
                    .Include(p => p.SalasDeCines)
                        .ThenInclude(p => p.Cine)
                    .Include(p => p.PeliculasActores.Where(w=> w.Actor.FechaNacimiento.Value.Year >= 1980))
                        .ThenInclude(pa => pa.Actor)
                    .FirstOrDefaultAsync(f => f.Id == id);

            if(pelicula is null)
                return NotFound();

            var peliculaDTO = mapper.Map<PeliculaDTO>(pelicula);
            peliculaDTO.Cines = peliculaDTO.Cines.DistinctBy(c => c.Id).ToList();

            return peliculaDTO;
        }

        [HttpGet("conprojectto/{id:int}")]
        public async Task<ActionResult<PeliculaDTO>> GetProjectTo(int id)
        {
            var pelicula = await context.Peliculas
                    .ProjectTo<PeliculaDTO>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(f => f.Id == id);

            if(pelicula is null)
                return NotFound();

            pelicula.Cines = pelicula.Cines.DistinctBy(c => c.Id).ToList();

            return pelicula;
        }

        [HttpGet("cargandoseSelectivo/{id:int}")]
        public async Task<ActionResult> GetSelectivo(int id)
        {
            var pelicula = await context.Peliculas.Select(p => 
            new
            {
                Id = p.Id,
                Titulo = p.Titulo,
                Generos = p.Generos.OrderByDescending(o => o.Nombre).ToList(),
                CantidadCines = p.SalasDeCines.Select(s => s.CineId).Distinct().Count()
            }).FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula is null)
            {
                return NotFound();
            }

            return Ok(pelicula);
        }

        [HttpGet("cargadoexplicito/{id:int}")]
        public async Task<ActionResult<PeliculaDTO>> GetExplicito(int id)
        {
            var pelicula = await context.Peliculas.AsTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula is null)
                return NotFound();

            //await context.Entry(pelicula).Collection(p=> p.Generos).LoadAsync();
            var cantidadGeneros = await context.Entry(pelicula).Collection(p => p.Generos).Query().CountAsync();

            var peliculaDTO = mapper.Map<PeliculaDTO>(pelicula);
            return peliculaDTO;
        }

        [HttpGet("lazyloading/{id:int}")]
        public async Task<ActionResult<PeliculaDTO>> GetLazyLoading(int id)
        {
            var pelicula = await context.Peliculas.AsTracking().FirstOrDefaultAsync(p => p.Id == id);

            if(pelicula is null) return NotFound();

            var peliculaDTO = mapper.Map<PeliculaDTO>(pelicula);
            peliculaDTO.Cines = peliculaDTO.Cines.DistinctBy(d => d.Id).ToList();
            return peliculaDTO;
        }

        [HttpGet("agrupadasPorEstreno")]
        public async Task<ActionResult> GetAgrupadaPorEstreno()
        {
            var peliculasAgrupadas = await context.Peliculas.GroupBy(p => p.EnCartelera)
                                        .Select(g => new
                                        {
                                            EnCartelera = g.Key,
                                            Conteo = g.Count(),
                                            Peliculas = g.ToList()
                                        }).ToListAsync();

            return Ok(peliculasAgrupadas);
        }

        [HttpGet("agrupadasPorCantidadDeGeneros")]
        public async Task<ActionResult> GetAgrupadaPorCantidadDeGeneros()
        {
            var peliculasAgrupadas = await context.Peliculas.GroupBy(p => p.Generos.Count())
                                        .Select(g => new
                                        {
                                            Conteo = g.Key,
                                            Titulos = g.Select(s => s.Titulo),
                                            Generos = g.Select(p => p.Generos).SelectMany(gen => gen).Select(s => s.Nombre).Distinct()
                                        }).ToListAsync();

            return Ok(peliculasAgrupadas);
        }

        [HttpGet("filtrar")]
        public async Task<ActionResult<List<PeliculaDTO>>>  Filtrar(
            [FromQuery] PeliculasFiltroDTO peliculasFiltroDTO)
        {
            var peliculasQueryable = context.Peliculas.AsQueryable();

            if(!string.IsNullOrEmpty(peliculasFiltroDTO.Titulo))
            {
                peliculasQueryable = peliculasQueryable.Where(p => p.Titulo.Contains(peliculasFiltroDTO.Titulo));
            }

            if(peliculasFiltroDTO.EnCartelera)
            {
                peliculasQueryable = peliculasQueryable.Where(p => p.EnCartelera);
            }

            if(peliculasFiltroDTO.ProximosEstrenos)
            {
                var hoy = DateTime.Now;
                peliculasQueryable = peliculasQueryable.Where(p => p.FechaEstreno > hoy);
            }

            if(peliculasFiltroDTO.GeneroId != 0)
            {
                peliculasQueryable = peliculasQueryable.Where(p => p.Generos.Select(s=> s.Id).Contains(peliculasFiltroDTO.GeneroId));
            }

            var peliculas = await peliculasQueryable.Include(p => p.Generos).ToListAsync();

            return mapper.Map<List<PeliculaDTO>>(peliculas);
        }

    }
}