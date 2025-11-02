using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Application.DTOs
{
    public class CitaDto
    {
        public int ID_Citas { get; set; }
        public int ID_Cliente { get; set; }
        public int ID_Estilista { get; set; }
        public int ID_Servicio { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool IsCanceled { get; set; }
    }
}
