using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class MServicio
    {
        [Key]
        public int ID_Servicio { get; set; }
        
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }

        public int DuracionMinutos { get; set; }


    }
}
