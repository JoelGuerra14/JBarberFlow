using JBF.Application.Dtos;
using JBF.Application.DTOs;
using JBF.Application.Interfaces;
using JBF.Application.Mappers;
using JBF.Domain.Base;
using Microsoft.Extensions.Logging;
using ReservaCitasBackend.Modelos;

namespace JBF.Application.Services
{
    public class CitaService : ICitaService
    {
        private readonly ICitasRepository _citasRepository;
        private readonly ILogger<CitaService> _logger;

        public CitaService(ICitasRepository citasRepository, ILogger<CitaService> logger)
        {
            _citasRepository = citasRepository;
            _logger = logger;
        }

        public async Task<OperationResult> CreateCitaAsync(CreateCitaDto createCitaDto)
        {
            try
            {
                if (createCitaDto.FechaInicio <= DateTime.UtcNow)
                {
                    _logger.LogWarning("Intento de crear cita en el pasado.");
                    return OperationResult.Failure("La fecha de inicio de la cita debe ser en el futuro.");
                }

                var nuevaCita = CitaMapper.ToCitasEntity(createCitaDto);
                var result = await _citasRepository.Createasync(nuevaCita);

                if (!result.IsSuccess)
                {
                    return result;
                }

                var citaDto = CitaMapper.ToCitaDto((MCitas)result.Data!);

                _logger.LogInformation("Cita creada exitosamente con ID {CitaId}", citaDto.ID_Citas);
                return OperationResult.Success("Cita creada exitosamente.", citaDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear la cita.");
                return OperationResult.Failure("Error inesperado al crear la cita.", ex);
            }
        }

        public async Task<OperationResult> UpdateCitaAsync(UpdateCitaDto updateCitaDto)
        {
            try
            {
                var citaActualizar = CitaMapper.ToCitasEntity(updateCitaDto);
                var result = await _citasRepository.Updateasync(citaActualizar);

                if (!result.IsSuccess)
                {
                    return result;
                }

                var citaDto = CitaMapper.ToCitaDto((MCitas)result.Data!);

                _logger.LogInformation("Cita ID {CitaId} actualizada.", citaDto.ID_Citas);
                return OperationResult.Success("Cita actualizada exitosamente.", citaDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar la cita ID {CitaId}", updateCitaDto.ID_Citas);
                return OperationResult.Failure("Error inesperado al actualizar la cita.", ex);
            }
        }

        public async Task<OperationResult> CancelCitaAsync(int id)
        {
            _logger.LogInformation("Iniciando cancelación (soft delete) de cita ID {CitaId}", id);
            try
            {
                var result = await _citasRepository.DeleteAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al cancelar la cita ID {CitaId}", id);
                return OperationResult.Failure("Error inesperado al cancelar la cita.", ex);
            }
        }

        public async Task<OperationResult> GetCitaByIdAsync(int id)
        {
            _logger.LogInformation("Buscando cita con ID {CitaId}", id);
            try
            {
                var result = await _citasRepository.GetbyIdasync(id);

                if (!result.IsSuccess)
                {
                    return result;
                }

                var citaDto = CitaMapper.ToCitaDto((MCitas)result.Data!);
                return OperationResult.Success(result.Message, citaDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al buscar la cita ID {CitaId}", id);
                return OperationResult.Failure("Error inesperado al buscar la cita.", ex);
            }
        }

        public async Task<OperationResult> GetAllCitasAsync()
        {
            try
            {
                var result = await _citasRepository.GetAllasync();

                if (!result.IsSuccess)
                {
                    return result;
                }

                var listaCitasDto = CitaMapper.ToCitasListDto((IEnumerable<MCitas>)result.Data!);
                return OperationResult.Success(result.Message, listaCitasDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener todas las citas.");
                return OperationResult.Failure("Error inesperado al obtener todas las citas.", ex);
            }
        }
    }
}