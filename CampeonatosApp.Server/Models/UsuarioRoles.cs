using System.ComponentModel.DataAnnotations;

namespace CampeonatosApp.Server.Models
{
    public partial class UsuarioRoles
    {
        [Key]
        public int Id { get; set; }
        public int UsuarioID { get; set; }
        public int RolesID { get; set; }


        //Claves foraneas
        public virtual Usuario? Usuario { get; set; }
        public virtual Roles? Roles { get; set; }

    }
}
