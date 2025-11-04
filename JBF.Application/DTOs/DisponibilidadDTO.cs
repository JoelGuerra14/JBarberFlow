using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Application.DTOs
{
    public class DisponibilidadDTO
    {
        public int ID_EstilistaDTO { get; set; } 
        public DayOfWeek DiaSemanaDTO { get; set; }
        public TimeSpan HoraInicioDTO { get; set; }
        public TimeSpan HoraFinDTO { get; set; }
    }
}
