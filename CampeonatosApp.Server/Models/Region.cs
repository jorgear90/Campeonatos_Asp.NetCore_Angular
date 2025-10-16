using System.ComponentModel.DataAnnotations;

namespace CampeonatosApp.Server.Models
{
    public partial class Region
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        //CLAVE FORANEA DE SALIDA
        public virtual ICollection<Comuna> Comunas { get; set; } = new List<Comuna>();
    }
}
