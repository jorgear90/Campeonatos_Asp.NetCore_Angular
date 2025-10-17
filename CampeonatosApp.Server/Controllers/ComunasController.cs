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
    public class ComunasController : ControllerBase
    {
        private readonly LocacionService _locacionService;

        public ComunasController(LocacionService locacionService)
        {
            _locacionService = locacionService;
        }

        [HttpGet("getComunas/{regionId}")]
        public async Task<IActionResult> GetComunas(int regionId)
        {
            try
            {
                var comunas = await _locacionService.ObtenerComunas(regionId);
                return Ok(comunas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }

}
