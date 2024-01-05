using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace EFCorePeliculas.Entidades
{
    public class Cine
    {
        public int Id { get; set; }

        [MaxLength(150)]
        [Required]
        public string Nombre {get; set; }

        // [Precision(precision:9, scale: 2)] // 9 digitos con 2 decimales
        // public decimal Precio {get; set; }

        public Point Ubicacion {get; set;}

        public CineOferta CineOferta{get;set;}
        public HashSet<SalaDeCine> SalasDeCine {get;set;}
    }
}