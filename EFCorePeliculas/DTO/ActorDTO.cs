using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCorePeliculas.DTO
{
    public class ActorDTO
    {
        public int Id {get;set;}
        public string Nombre {get;set;}
        public DateTime? FechaNacimiento {get;set;}
    }
}