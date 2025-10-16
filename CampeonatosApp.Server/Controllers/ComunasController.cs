using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CampeonatosApp.Server.Data;
using CampeonatosApp.Server.Models;

namespace CampeonatosApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComunasController : Controller
    {
        private readonly AppDbContext _context;

        public ComunasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("getComunas/{regionId}")]
        public async Task<IActionResult> GetComunasPorRegion(int regionId)
        {
            var comunas = await _context.Comunas.Where(c => c.Region.Id == regionId).Select(c => new { c.Id, c.Nombre }).ToListAsync();

            return Ok(comunas);
        }

    }
}
