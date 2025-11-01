using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MEstilista
    {
        [Key]
        public int ID_Estilista { get; set; }
        public string? Nombre { get; set; }
        public string? Especialidad { get; set; }
        public string Email { get; set; }
    }
}
