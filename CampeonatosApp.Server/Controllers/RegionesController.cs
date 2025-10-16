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
    public class RegionesController : Controller
    {
        private readonly AppDbContext _context;

        public RegionesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("getRegiones")]
        public async Task<IActionResult> GetRegiones()
        {
            var regiones = await _context.Regiones.Select(r => new { r.Id, r.Nombre }).ToListAsync();

            return Ok(regiones);
        }

    }

}
