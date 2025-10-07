using CampeonatosApp.Server.Data;
using CampeonatosApp.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace CampeonatosApp.Server.Services
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        public async Task<bool> CrearUsuario(string correo, string password)
        {
            if (_context.Usuarios.Any(u => u.Correo == correo))
                return false;

            var usuario = new Usuario { Correo = correo };
            usuario.Contraseña = _passwordHasher.HashPassword(usuario, password);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Login(string correo, string password)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuario == null) return false;

            var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.Contraseña, password);
            return result == PasswordVerificationResult.Success;
        }

    }
}
