using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCorePeliculas.DTO
{
    public class PeliculasFiltroDTO
    {
        public string Titulo { get; set; }
        
        public int GeneroId { get; set; }

        public bool EnCartelera { get; set; }
        public bool ProximosEstrenos { get; set; }
    }         
}