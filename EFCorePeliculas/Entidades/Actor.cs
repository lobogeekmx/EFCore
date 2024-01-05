using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFCorePeliculas.Entidades
{
    public class Actor
    {
        public int Id {get;set;}

        [MaxLength(150)]
        [Required]
        public string Nombre {get;set;}
        public string Biografia {get;set;}

        [Column(TypeName ="Date")]
        public DateTime? FechaNacimiento {get;set;}

        public HashSet<PeliculaActor> PeliculasActores {get;set;}
    }
}