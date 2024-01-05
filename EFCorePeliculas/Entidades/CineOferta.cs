using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas.Entidades
{
    public class CineOferta
    {
        public int Id{get;set;}

        [Column(TypeName ="Date")]
        public DateTime FechaInicio{get;set;}
        
        [Column(TypeName ="Date")]
        public DateTime FechaFin{get;set;}

        [Precision(precision:5, scale:2)]
        public decimal PorcentajeDescuento{get;set;}

        public int CineId {get;set;}
        
    }
}