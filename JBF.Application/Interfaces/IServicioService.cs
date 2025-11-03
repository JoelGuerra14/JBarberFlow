using JBF.Application.DTOs;
using JBF.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Application.Interfaces
{
    public interface IServicioService
    {
        Task<OperationResult> GetByIdAsync(int id);
        Task<OperationResult> GetAllAsync();
        Task<OperationResult> CreateAsync(CreateServicioDTO createServicioDto);
        Task<OperationResult> UpdateAsync(int id, CreateServicioDTO ServicioDto);
        Task<OperationResult> DeleteAsync(int id);
    }
}
