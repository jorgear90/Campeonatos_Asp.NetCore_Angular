using System.ComponentModel.DataAnnotations;

namespace CampeonatosApp.Server.Models
{
    public partial class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Cotraseña { get; set; }

        //CLAVE FORANEA DE SALIDA
        public virtual ICollection<UsuarioRoles> UsuariosRoles { get; set; } = new List<UsuarioRoles>();

        public virtual ICollection<Jugador> Jugadores { get; set; } = new List<Jugador>();
    }
}
