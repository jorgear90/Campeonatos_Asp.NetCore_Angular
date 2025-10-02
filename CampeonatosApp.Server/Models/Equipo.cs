using System.ComponentModel.DataAnnotations;

namespace CampeonatosApp.Server.Models
{
    public partial class Equipo
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string RutaLogo { get; set; }
    }
}
