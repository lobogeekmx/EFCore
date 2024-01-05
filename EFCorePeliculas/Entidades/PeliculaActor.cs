using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCorePeliculas.Entidades
{
    public class PeliculaActor
    {
        public int PeliculaId { get; set; }
        public int ActorId { get; set; }

        [MaxLength(150)]
        public string Personaje { get; set;}
        public int Orden {get;set;}
        public Pelicula Pelicula {get;set;}
        public Actor Actor {get;set;}


    }
}