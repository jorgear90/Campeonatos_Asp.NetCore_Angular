using CampeonatosApp.Server.Data;
using CampeonatosApp.Server.Models;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Claims;
using static CampeonatosApp.Server.Controllers.EquiposController;

namespace CampeonatosApp.Server.Services
{
    public class EquipoService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EquipoService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CrearEquipo(string nombre, IFormFile logo, int comunaId)
        {
            var correo = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(correo))
            {
                throw new UnauthorizedAccessException("Usuario no autenticado");
            }

            var idUsuario = await _context.Usuarios.Where(u => u.Correo == correo).Select(u => u.Id).FirstOrDefaultAsync(); ;

            if (idUsuario == 0)
            {
                throw new Exception($"Usuario con correo {correo} no encontrado");
            }

            var equiposUsuario = await _context.Equipos.Where(e => e.UsuarioID == idUsuario).ToListAsync();

            if (equiposUsuario.Count >= 10)
            {
                return false;
            }

            var equipo = new Equipo { Nombre = nombre, RutaLogo = $"/logos/sin-logo/pngwing.com.png", UsuarioID = idUsuario, ComunaID = comunaId };

            _context.Equipos.Add(equipo);
            await _context.SaveChangesAsync();

            var idEquipo = equipo.Id;

            if (logo != null && logo.Length > 0)
            {
                var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logos", "equipos", idEquipo.ToString());

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                var filePath = Path.Combine(carpeta, logo.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await logo.CopyToAsync(stream);
                }

                equipo.RutaLogo = $"/logos/equipos/{idEquipo.ToString()}/{logo.FileName}";


                _context.Equipos.Update(equipo);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> EditarEquipo(int id, string nombre, int comunaId, IFormFile logo)
        {
            /*var correo = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(correo))
            {
                throw new UnauthorizedAccessException("Usuario no autenticado");
            }

            var idUsuario = await _context.Usuarios.Where(u => u.Correo == correo).Select(u => u.Id).FirstOrDefaultAsync(); ;

            if (idUsuario == 0)
            {
                throw new Exception($"Usuario con correo {correo} no encontrado");
            }*/

            var equipo = await _context.Equipos.Where(e => e.Id == id).FirstOrDefaultAsync();

            //var equipo = new Equipo { Nombre = nombre, RutaLogo = $"/logos/sin-logo/pngwing.com.png", UsuarioID = idUsuario, ComunaID = comunaId };

            equipo.ComunaID = comunaId;
            equipo.Nombre = nombre;

            _context.Equipos.Update(equipo);
            await _context.SaveChangesAsync();

            var editar = 1;

            await crearLogo(logo, equipo.Id, editar);

            return true;
        }

        public async Task crearLogo(IFormFile logo, int id, int editar)
        {
            var equipo = await _context.Equipos.FirstOrDefaultAsync(e => e.Id == id);

            if (logo != null && logo.Length > 0)
            {
                // eliminar imagen anterior
                if (editar == 1 && equipo.RutaLogo != "/logos/sin-logo/pngwing.com.png")
                {
                    var rutaFisica = Path.Combine("wwwroot", equipo.RutaLogo.TrimStart('/'));
                    if (System.IO.File.Exists(rutaFisica))
                    {
                        System.IO.File.Delete(rutaFisica);
                    }
                }

                var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logos", "equipos", id.ToString());

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                var filePath = Path.Combine(carpeta, logo.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await logo.CopyToAsync(stream);
                }

                equipo.RutaLogo = $"/logos/equipos/{id}/{logo.FileName}";
                _context.Equipos.Update(equipo);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<List<EquipoDto>> ObtenerEquipos(int? regionUsuarioId, int page = 1, int pageSize = 10)
        {

            var query = _context.Equipos.Include(e => e.Comuna).ThenInclude(c => c.Region).AsQueryable();

            if (regionUsuarioId.HasValue)
            {
                query = query
                    .OrderByDescending(e => e.Comuna.RegionID == regionUsuarioId.Value) // primero su región
                    .ThenBy(e => e.ComunaID) // dentro, ordenar por ComunaID asc
                    .ThenBy(e => e.Nombre); // dentro, por nombre asc
            }
            else
            {
                query = query.OrderBy(e => e.Nombre); // usuario no autenticado
            }

            var listaEquipos = await query.Skip((page - 1) * pageSize).Take(pageSize)
                .Select(e => new EquipoDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    RutaLogo = e.RutaLogo,
                    Comuna = e.Comuna != null ? e.Comuna.Nombre : "Sin comuna",
                    Region = e.Comuna != null && e.Comuna.Region != null ? e.Comuna.Region.Nombre : "Sin región",
                    RegionID = e.Comuna != null && e.Comuna.Region != null ? e.Comuna.Region.Id : 0,
                    ComunaID = e.ComunaID
                })
                .ToListAsync();

            return listaEquipos;

        }

        public async Task<(List<EquipoDto> equipos, int totalPages)> ObtenerEquiposFiltrados(int? regionUsuarioId, int? regionId, int? comunaId, int page = 1, int pageSize = 10, string? searchTerm = null)
        {
            var query = _context.Equipos
                .Include(e => e.Comuna)
                .ThenInclude(c => c.Region)
                .AsQueryable();

            // 🔍 Si hay término de búsqueda
            if (!string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(e => e.Nombre.ToLower().Contains(searchTerm.ToLower()));

            // 📍 Filtro de región/comuna
            if (regionId.HasValue)
                query = query.Where(e => e.Comuna.Region.Id == regionId.Value);

            if (comunaId.HasValue)
            {
                if(comunaId.Value != 0)
                {
                    query = query.Where(e => e.Comuna.Id == comunaId.Value);
                }
            }
                

            // 👤 Jerarquía por región del usuario
            if (regionUsuarioId.HasValue)
                query = query
                    .OrderByDescending(e => e.Comuna.RegionID == regionUsuarioId.Value)
                    .ThenBy(e => e.ComunaID)
                    .ThenBy(e => e.Nombre);
            else
                query = query.OrderBy(e => e.Nombre);

            var totalEquipos = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalEquipos / (double)pageSize);

            var equipos = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new EquipoDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    RutaLogo = e.RutaLogo,
                    Comuna = e.Comuna != null ? e.Comuna.Nombre : "Sin comuna",
                    Region = e.Comuna != null && e.Comuna.Region != null ? e.Comuna.Region.Nombre : "Sin región",
                    RegionID = e.Comuna != null && e.Comuna.Region != null ? e.Comuna.Region.Id : 0,
                    ComunaID = e.ComunaID
                })
                .ToListAsync();

            return (equipos, totalPages);
        }


        public async Task<List<EquipoDto>> ObtenerMisEquipos()
        {
            var correo = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(correo))
            {
                throw new UnauthorizedAccessException("Usuario no autenticado");
            }

            var idUsuario = await _context.Usuarios.Where(u => u.Correo == correo).Select(u => u.Id).FirstOrDefaultAsync();

            if (idUsuario == 0)
            {
                throw new Exception($"Usuario con correo {correo} no encontrado");
            }

            var listaEquipos = await _context.Equipos.Where(e => e.UsuarioID == idUsuario).Include(e => e.Comuna).ThenInclude(c => c.Region)
                .Select(e => new EquipoDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    RutaLogo = e.RutaLogo,
                    Comuna = e.Comuna != null ? e.Comuna.Nombre : "Sin comuna",
                    Region = e.Comuna != null && e.Comuna.Region != null ? e.Comuna.Region.Nombre : "Sin región",
                })
                .ToListAsync();

            return listaEquipos;

        }

        public async Task<EquipoDto> ObtenerMiEquipo(int id)
        {
            var equipo = await _context.Equipos.Where(e => e.Id == id).Include(e => e.Comuna).ThenInclude(c => c.Region).Select(e => new EquipoDto
            {
                Id = e.Id,
                Nombre = e.Nombre,
                RutaLogo = e.RutaLogo,
                Comuna = e.Comuna != null ? e.Comuna.Nombre : "Sin comuna",
                Region = e.Comuna != null && e.Comuna.Region != null ? e.Comuna.Region.Nombre : "Sin región",
            }).FirstOrDefaultAsync();

            return equipo;

        }
    }
}
