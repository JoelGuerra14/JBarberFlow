using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Application.DTOs
{
    public class CreateServicioDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, 10000.00, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La duración es obligatoria")]
        [Range(5, 90, ErrorMessage = "La duración debe estar entre 5 y 90 minutos")]
        public int DuracionMinutos { get; set; }
    }
}
