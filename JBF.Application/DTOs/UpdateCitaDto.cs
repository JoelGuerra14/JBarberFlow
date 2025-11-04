using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Application.DTOs
{
    public class UpdateCitaDto
    {
        [Required]
        public int ID_Citas { get; set; }
        [Required]
        public int ID_Cliente { get; set; }
        [Required]
        public int ID_Estilista { get; set; }
        [Required]
        public int ID_Servicio { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }
    }
}
