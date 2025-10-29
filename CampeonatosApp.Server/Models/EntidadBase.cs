using System.ComponentModel.DataAnnotations;

namespace CampeonatosApp.Server.Models
{
    public abstract class EntidadBase
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
