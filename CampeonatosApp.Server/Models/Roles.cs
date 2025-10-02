using System.ComponentModel.DataAnnotations;

namespace CampeonatosApp.Server.Models
{
    public partial class Roles
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        //CLAVE FORANEA DE SALIDA
        public virtual ICollection<UsuarioRoles> UsuariosRoles { get; set; } = new List<UsuarioRoles>();
    }
}
