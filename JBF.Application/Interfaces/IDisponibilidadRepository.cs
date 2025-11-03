using Domain.Entities;
using JBF.Application.Base;
using JBF.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Application.Interfaces
{
    public interface IDisponibilidadRepository : IRepositoryBase<MDisponibilidad>
    {
        Task<OperationResult> GetByDisponibilidadIdAsync(int id);
    }
}
