using JBF.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace ReservaCitasBackend.Modelos
{
    public class MUsers : AuditEntity
    {
        [Key]
        public int ID_User { get; set; }
        public string? NombreUsuario { get; set; }
        public string? PasswordHash { get; set; }
        public string? Correo { get; set; }
        public string? TipoUsuario { get; set; }
        public bool IsDeleted { get; set; }
    }
}
