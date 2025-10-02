using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampeonatosApp.Server.Models
{
    public partial class Jugador
    {
        [Key]
        public int Id { get; set; }
        public string Rut {  get; set; }
        public string RutaCopiaDocumento { get; set; }
        public string Nombre {  get; set; }
        public string Apellido { get; set; }
        public string AnioNacimiento { get; set; }
        public string Correo {  get; set; }
        public int UsuarioID { get; set; }


        //Clave foranea
        [ForeignKey(nameof(UsuarioID))]
        public virtual Usuario? Usuario { get; set; }
    }
}
