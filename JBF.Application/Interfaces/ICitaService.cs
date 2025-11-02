using JBF.Application.Dtos;
using JBF.Application.DTOs;
using JBF.Domain.Base;

namespace JBF.Application.Interfaces
{
    public interface ICitaService
    {
        Task<OperationResult> CreateCitaAsync(CreateCitaDto createCitaDto);
        Task<OperationResult> UpdateCitaAsync(UpdateCitaDto updateCitaDto);
        Task<OperationResult> CancelCitaAsync(int id);
        Task<OperationResult> GetCitaByIdAsync(int id);
        Task<OperationResult> GetAllCitasAsync();
    }
}
