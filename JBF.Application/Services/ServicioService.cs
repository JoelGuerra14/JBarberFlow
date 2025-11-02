using Domain.Entities;
using JBF.Application.Base;
using JBF.Application.DTOs;
using JBF.Application.Interfaces;
using JBF.Domain.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Application.Services
{
    public class ServicioService : IServicioService
    {
        private readonly IServicioRepository _servicioRepo;
        private readonly ILogger<MServicio> _logger;
        public ServicioService(IServicioRepository servicioRepo, ILogger<MServicio> logger)
        {
            _servicioRepo = servicioRepo;
            _logger = logger;
        }
        public async Task<OperationResult> CreateAsync(CreateServicioDTO dto)
        {
            _logger.LogInformation($"Intentando crear servicio: {dto.Nombre}");
            try
            {

                var entidad = new MServicio
                {
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    Precio = dto.Precio,
                    DuracionMinutos = dto.DuracionMinutos
                };

                var result = await _servicioRepo.Createasync(entidad);

                if (!result.IsSuccess)
                {
                    _logger.LogError($"Error al crear servicio: {result.Message}");
                    return result;
                }

                var dtoCreado = MapToDto(result.Data as MServicio);
                _logger.LogInformation($"Servicio '{dto.Nombre}' creado con ID: {dtoCreado.ID_Servicio}");

                return OperationResult.Success("Servicio creado con éxito.", dtoCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al crear servicio: {ex.Message}", ex);
                return OperationResult.Failure($"Error inesperado: {ex.Message}");
            }
        }

        public async Task<OperationResult> UpdateAsync(int id, CreateServicioDTO dto)
        {
            _logger.LogInformation($"Intentando actualizar servicio ID: {id}");
            try
            {
                var resultRepo = await _servicioRepo.GetbyIdasync(id);
                if (!resultRepo.IsSuccess)
                {
                    return OperationResult.Failure($"Servicio con ID {id} no encontrado.");
                }

                var entidad = resultRepo.Data as MServicio;

                entidad.Nombre = dto.Nombre;
                entidad.Descripcion = dto.Descripcion;
                entidad.Precio = dto.Precio;
                entidad.DuracionMinutos = dto.DuracionMinutos;

                var updateResult = await _servicioRepo.Updateasync(entidad);
                if (!updateResult.IsSuccess)
                {
                    _logger.LogError($"Error al actualizar servicio: {updateResult.Message}");
                    return updateResult;
                }

                _logger.LogInformation($"Servicio ID: {id} actualizado.");
                return OperationResult.Success("Servicio actualizado con éxito.", MapToDto(entidad));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al actualizar servicio ID {id}: {ex.Message}", ex);
                return OperationResult.Failure($"Error inesperado: {ex.Message}");
            }
        }

        public async Task<OperationResult> GetAllAsync()
        {
            _logger.LogInformation("Obteniendo todos los servicios");
            var result = await _servicioRepo.GetAllasync();

            if (!result.IsSuccess) return result;

            var listaEntidades = result.Data as IEnumerable<MServicio>;
            var listaDto = listaEntidades.Select(MapToDto).ToList();

            return OperationResult.Success("Servicios obtenidos con éxito.", listaDto);
        }

        public async Task<OperationResult> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Obteniendo servicio ID: {id}");
            var result = await _servicioRepo.GetbyIdasync(id);

            if (!result.IsSuccess) return result;

            var dto = MapToDto(result.Data as MServicio);
            return OperationResult.Success("Servicio obtenido con éxito.", dto);
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            _logger.LogInformation($"Intentando eliminar servicio ID: {id}");

            var result = await _servicioRepo.Deleteasync(id);

            if (!result.IsSuccess)
            {
                _logger.LogWarning($"La operación del servicio DeleteAsync falló para el ID: {id}. Mensaje: {result.Message}");
            }

            return result;
        }
        private ServicioDTO MapToDto(MServicio entidad)
        {
            if (entidad == null) return null;

            return new ServicioDTO
            {
                ID_Servicio = entidad.ID_Servicio,
                Nombre = entidad.Nombre,
                Descripcion = entidad.Descripcion,
                Precio = entidad.Precio,
                DuracionMinutos = entidad.DuracionMinutos
            };
        }
    }
}
