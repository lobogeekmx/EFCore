using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas.Entidades
{
    public class Pelicula
    {

        public int Id {get;set;}

        [MaxLength(250)]
        [Required]
        public string Titulo {get;set;}

        public bool EnCartelera {get;set;}

        [Column(TypeName ="Date")]
        public DateTime FechaEstreno {get;set;}

        [MaxLength(500)]
        [Unicode(false)]
        public string PosterUrl {get; set;}

        public HashSet<Genero> Generos {get;set;}
        public HashSet<SalaDeCine> SalasDeCines {get;set;}
        public HashSet<PeliculaActor> PeliculasActores {get;set;}
    }
}