using System.ComponentModel.DataAnnotations;

namespace CampeonatosApp.Server.Models
{
    public partial class Roles : EntidadBase
    {
        public string Descripcion { get; set; }

        //CLAVE FORANEA DE SALIDA
        public virtual ICollection<UsuarioRoles> UsuariosRoles { get; set; } = new List<UsuarioRoles>();
    }
}
