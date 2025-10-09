using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampeonatosApp.Server.Data;
using CampeonatosApp.Server.Models;
using CampeonatosApp.Server.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CampeonatosApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UsuarioService _usuarioService;
        private readonly IConfiguration _config;

        public UsuariosController(UsuarioService usuarioService, IConfiguration config)
        {
            _usuarioService = usuarioService;
            _config = config;
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
            var valido = await _usuarioService.Login(dto.Correo, dto.Contraseña);
            if (!valido) return Unauthorized(new { message = "Correo o contraseña incorrecta" });

            var token = GenerarJwtToken(dto.Correo);
            return Ok(new { token });
        }

        private string GenerarJwtToken(string correo)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, correo),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public class UsuarioDto
        {
            public string Correo { get; set; } = string.Empty;
            public string Contraseña { get; set; } = string.Empty;
        }
    }
}
