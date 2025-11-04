using JBF.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MEstilista : AuditEntity
    {
        [Key]
        public int ID_Estilista { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public int ID_Servicio { get; set; }
    }
}