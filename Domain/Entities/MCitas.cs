using JBF.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace ReservaCitasBackend.Modelos
{
    public class MCitas : AuditEntity
    {
        [Key]
        public int ID_Citas {  get; set; }
        public int ID_Cliente { get; set; }
        public int ID_Estilista { get; set; }
        public int ID_Servicio { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
