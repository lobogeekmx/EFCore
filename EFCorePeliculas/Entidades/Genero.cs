using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas.Entidades
{
    [Table("Generos", Schema ="dbo")]
    public class Genero
    {
        [Key]
        public int Id {get;set;}

        [MaxLength(150)]
        [Required]
        [Column("Nombre")]
        public string Nombre{get;set;}

        public HashSet<Pelicula> Peliculas {get;set;}
    }
}