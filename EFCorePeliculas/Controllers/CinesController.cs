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
    [Route("api/[controller]")]
    public class CinesController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public CinesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CineDTO>> Get()
        {
            return await context.Cines
                    .ProjectTo<CineDTO>(mapper.ConfigurationProvider)
                    .ToListAsync();
        }
    }
}