using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampeonatosApp.Server.Data;
using CampeonatosApp.Server.Models;
using CampeonatosApp.Server.Services;

namespace CampeonatosApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioDto dto)
        {
            var result = await _usuarioService.CrearUsuario(dto.Correo, dto.Contraseña);
            if (!result) return BadRequest("El correo ya existe");
            return Ok(new { message = "Usuario creado correctamente" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDto dto)
        {
            var result = await _usuarioService.Login(dto.Correo, dto.Contraseña);
            if (!result) return Unauthorized("Correo o contraseña incorrecta");
            return Ok(new { message = "Login correcto" });
        }

        public class UsuarioDto
        {
            public string Correo { get; set; } = string.Empty;
            public string Contraseña { get; set; } = string.Empty;
        }
    }
}
