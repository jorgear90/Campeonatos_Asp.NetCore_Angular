using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampeonatosApp.Server.Models
{
    public partial class Comuna
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int RegionID { get; set; }

        //Clave foranea
        [ForeignKey(nameof(RegionID))]
        public virtual Region? Region { get; set; }

        //CLAVE FORANEA DE SALIDA
        public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
    }
}
