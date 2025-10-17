using CampeonatosApp.Server.Data;
using CampeonatosApp.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CampeonatosApp.Server.Controllers.EquiposController;

namespace CampeonatosApp.Server.Services
{
    public class LocacionService
    {
        private readonly AppDbContext _context;

        public LocacionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ComunaDto>> ObtenerComunas(int regionId)
        {
            var comunas = await _context.Comunas
                .Where(c => c.RegionID == regionId)
                .Select(c => new ComunaDto { Id = c.Id, Nombre = c.Nombre })
                .OrderBy(c => c.Nombre)
                .ToListAsync();

            return comunas;
        }
    }

    public class ComunaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }


}
