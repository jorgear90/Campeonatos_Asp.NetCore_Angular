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
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using System.Drawing.Printing;
using System.Security.Claims;

namespace CampeonatosApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquiposController : Controller
    {
        private readonly AppDbContext _context;
        private readonly EquipoService _equipoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EquiposController(AppDbContext context, EquipoService equipoService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _equipoService = equipoService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpPost("createTeams")]
        public async Task<IActionResult> CreateTeams([FromForm] CrearEquipoDto dto)
        {
            try
            {
                var result = await _equipoService.CrearEquipo(dto.Nombre, dto.Logo, dto.ComunaId);

                if (!result)
                    return BadRequest("Solo puede registrar hasta 10 equipos");

                return Ok(new { message = "Equipo creado correctamente" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado en el servidor." });
            }
        }



        [HttpGet("teamNames")]
        public async Task<IActionResult> GetTeamNames(int? regionId = null, int? comunaId = null)
        {
            try
            {
                var query = _context.Equipos
                    .Include(e => e.Comuna)
                    .ThenInclude(c => c.Region)
                    .AsQueryable();

                if (regionId.HasValue)
                    query = query.Where(e => e.Comuna.Region.Id == regionId.Value);

                if (comunaId.HasValue)
                    query = query.Where(e => e.ComunaID == comunaId.Value);

                var nombres = await query
                    .Select(e => e.Nombre)
                    .OrderBy(n => n)
                    .ToListAsync();

                return Ok(new { nombres });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("getTeams")]
        public async Task<IActionResult> GetTeams(int page = 1, int? regionId = null, int? comunaId = null, string? searchTerm = null)
        {
            try
            {
                int pageSize = 10;
                int? regionUsuarioId = null;

                // Obtener región del usuario autenticado
                var userClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
                if (userClaim != null)
                {
                    int userId = await _context.Usuarios
                        .Where(u => u.Correo == userClaim)
                        .Select(u => u.Id)
                        .FirstOrDefaultAsync();

                    var usuario = await _context.Usuarios
                        .Include(u => u.Comuna)
                        .ThenInclude(c => c.Region)
                        .FirstOrDefaultAsync(u => u.Id == userId);

                    if (usuario?.Comuna?.Region != null)
                        regionUsuarioId = usuario.Comuna.Region.Id;
                }

                var (equipos, totalPages) = await _equipoService.ObtenerEquiposFiltrados(
                    regionUsuarioId, regionId, comunaId, page, pageSize, searchTerm);

                return Ok(new
                {
                    message = "Equipos obtenidos correctamente",
                    equipos,
                    totalPages
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchTeams([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return BadRequest(new { message = "Debe ingresar un nombre válido" });

            var equipos = await _context.Equipos
                .Include(e => e.Comuna)
                .ThenInclude(c => c.Region)
                .Where(e => e.Nombre.ToLower().Contains(nombre.ToLower()))
                .Select(e => new
                {
                    e.Id,
                    e.Nombre,
                    Comuna = e.Comuna.Nombre,
                    Region = e.Comuna.Region.Nombre,
                    e.RutaLogo
                })
                .Take(20)
                .ToListAsync();

            return Ok(new { equipos });
        }


        [HttpGet("getMyTeams")]
        public async Task<IActionResult> getMyTeams()
        {
            try
            {
                var equipos = await _equipoService.ObtenerMisEquipos();

                if (equipos == null || equipos.Count == 0)
                    return Ok(new { message = "No tienes equipos para mostrar", equipos = new List<Equipo>() });

                return Ok(new { message = "Equipos obtenidos correctamente", equipos });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Usuario no autenticado" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        public class EquipoDto
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string? RutaLogo { get; set; }
            public string Comuna { get; set; }
            public string Region { get; set; }
            public int RegionID { get; set; }
            public int ComunaID { get; set; }
        }

        public class CrearEquipoDto
        {
            [FromForm] public string Nombre { get; set; } = string.Empty;
            [FromForm] public int ComunaId { get; set; }
            [FromForm] public IFormFile? Logo { get; set; }
        }
    }
}
