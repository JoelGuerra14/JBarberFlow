using System.ComponentModel.DataAnnotations;

namespace ReservaCitasBackend.Modelos
{
    public class MUsers
    {
        [Key]
        public int ID_Users { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Pass { get; set; }
        public string? Correo { get; set; }
        public string? TipoUsuario { get; set; }
        public bool CitaReservada { get; set; }
    }
}
