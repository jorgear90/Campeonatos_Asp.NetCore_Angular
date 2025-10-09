using System.ComponentModel.DataAnnotations;

namespace CampeonatosApp.Server.Models
{
    public partial class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;

        //CLAVE FORANEA DE SALIDA
        public virtual ICollection<UsuarioRoles> UsuariosRoles { get; set; } = new List<UsuarioRoles>();
        public virtual ICollection<Jugador> Jugadores { get; set; } = new List<Jugador>();
        public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
    }
}
