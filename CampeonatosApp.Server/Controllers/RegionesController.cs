using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CampeonatosApp.Server.Data;
using CampeonatosApp.Server.Models;
using CampeonatosApp.Server.Services;

namespace CampeonatosApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly LocacionService _locacionService;

        public RegionesController(AppDbContext context, LocacionService locacionService)
        {
            _context = context;
            _locacionService = locacionService;
        }

        [HttpGet("getRegiones")]
        public async Task<IActionResult> GetComunas()
        {
            try
            {
                var regiones = await _locacionService.ObtenerRegiones();
                return Ok(regiones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }

}
