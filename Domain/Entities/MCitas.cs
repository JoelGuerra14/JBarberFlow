using JBF.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaCitasBackend.Modelos
{
    public class MCitas
    {
        [Key]
        public int ID_Citas {  get; set; }
        public int ID_Cliente { get; set; }
        public int ID_Estilista { get; set; }
        public int ID_Servicio { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool IsCanceled { get; set; } = false;
    }
}
