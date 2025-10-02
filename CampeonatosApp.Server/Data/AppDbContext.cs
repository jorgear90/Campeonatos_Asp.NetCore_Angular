using CampeonatosApp.Server.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace CampeonatosApp.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UsuarioRoles> UsuariosRoles { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Jugador> Jugadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //UsuarioRoles
            modelBuilder.Entity<UsuarioRoles>()
                .HasOne(t => t.Usuario)
                .WithMany(p => p.UsuariosRoles)
                .HasForeignKey(t => t.UsuarioID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UsuarioRoles>()
               .HasOne(t => t.Roles)
               .WithMany(p => p.UsuariosRoles)
               .HasForeignKey(t => t.RolesID)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
