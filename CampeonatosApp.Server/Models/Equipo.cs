using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampeonatosApp.Server.Models
{
    public partial class Equipo : EntidadBase
    {
        public string? RutaLogo { get; set; }
        public int ComunaID {  get; set; }
        public int UsuarioID { get; set; }

        //Clave foranea
        public virtual Usuario? Usuario { get; set; }
        public virtual Comuna? Comuna { get; set; }
    }
}
