using Domain.Entities;
using JBF.Application.DTOs;
using JBF.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Application.Interfaces
{
    public interface IDisponibilidad
    {
        public Task<OperationResult> GetbyIdasync(int id);
        public Task<OperationResult> GetAllasync();
        public Task<OperationResult> Createasync(DisponibilidadDTO disponibilidadDTO);
        public Task<OperationResult> Updateasync(int id, DisponibilidadDTO disponibilidadDTO);
    }
}
