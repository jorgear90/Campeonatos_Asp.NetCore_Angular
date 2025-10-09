using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampeonatosApp.Server.Models
{
    public partial class Equipo
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string RutaLogo { get; set; }
        public int UsuarioID { get; set; }

        //Clave foranea
        [ForeignKey(nameof(UsuarioID))]
        public virtual Usuario? Usuario { get; set; }
    }
}
