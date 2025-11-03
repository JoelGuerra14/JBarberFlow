using System.ComponentModel.DataAnnotations;

namespace JBF.Application.Dtos
{
    public class CreateCitaDto
    {
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