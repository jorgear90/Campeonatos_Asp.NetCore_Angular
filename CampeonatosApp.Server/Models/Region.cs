using System.ComponentModel.DataAnnotations;

namespace CampeonatosApp.Server.Models
{
    public partial class Region : EntidadBase
    {

        //CLAVE FORANEA DE SALIDA
        public virtual ICollection<Comuna> Comunas { get; set; } = new List<Comuna>();
    }
}
