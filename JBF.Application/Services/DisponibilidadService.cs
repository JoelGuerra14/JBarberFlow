using Domain.Entities;
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
    public class DisponibilidadService : IDisponibilidad
    {
        private readonly IDisponibilidadRepository _repository;
        private readonly ILogger<DisponibilidadService> _logger;

        public DisponibilidadService(IDisponibilidadRepository repository, ILogger<DisponibilidadService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        //Crear disponibilidad
        public async Task<OperationResult> Createasync(DisponibilidadDTO disponibilidadDTO)
        {
            _logger.LogInformation("Intentando crear disponibilidad de estilista");

            try
            {
                if (disponibilidadDTO == null)
                {
                    return OperationResult.Failure("Los datos de disponibilidad son null");
                }

                var Mapeo = MapeoEntidad(disponibilidadDTO);

                var resultado = await _repository.Createasync(Mapeo);

                if (!resultado.IsSuccess)
                {
                    _logger.LogError($"Error al crear disponibilidad: {resultado.Message}");
                    return resultado;
                }

                _logger.LogInformation("Disponibilidad creada con exito");
                return OperationResult.Success("Disponibilidad creada con exito", resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al crear disponibilidad: {ex.Message}");
                return OperationResult.Failure($"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        //Actualizar disponibilidad
        public async Task<OperationResult> Updateasync(int id, DisponibilidadDTO disponibilidadDTO)
        {
            try
            {
                _logger.LogInformation($"Intentando actualizar disponibilidad con id {id}");

                if (disponibilidadDTO == null)
                    return OperationResult.Failure("Los datos enviados para actualizar son null");

                var verificar = await _repository.GetByDisponibilidadIdAsync(id);

                if (!verificar.IsSuccess || verificar.Data == null)
                {
                    return OperationResult.Failure($"Disponibilidad con id {id} no encontrada");
                }

                var disponibilidadActualizar = verificar.Data as MDisponibilidad;

                // Mapear los valores del DTO sobre la entidad existente
                disponibilidadActualizar.DiaSemana = disponibilidadDTO.DiaSemanaDTO;
                disponibilidadActualizar.HoraInicio = disponibilidadDTO.HoraInicioDTO;
                disponibilidadActualizar.HoraFin = disponibilidadDTO.HoraFinDTO;

                var actualizar = await _repository.Updateasync(disponibilidadActualizar);

                if (!actualizar.IsSuccess)
                {
                    _logger.LogError($"Error al actualizar disponibilidad: {actualizar.Message}");
                    return actualizar;
                }

                _logger.LogInformation($"Disponibilidad con id {id} actualizada con exito");
                return OperationResult.Success("Disponibilidad actualizada", actualizar.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al actualizar disponibilidad: {ex.Message}");
                return OperationResult.Failure($"Ocurrio un error inesperado: {ex.Message}");
            }
        }

        //Obtener disponibilidad por id
        public async Task<OperationResult> GetbyIdasync(int id)
        {
            try
            {
                _logger.LogInformation($"Intentando recuperar disponibilidad con id {id}");

                var resultado = await _repository.GetbyIdasync(id);

                if (!resultado.IsSuccess)
                {
                    return resultado;
                }

                var listaDTO = (resultado.Data as List<MDisponibilidad>)
                    .Select(d => MapeoDTO(d)).ToList();

                if (listaDTO == null || listaDTO.Count == 0) 
                {
                    return OperationResult.Failure("No se pudieron mapear las disponibilidades del estilista");
                }

                return OperationResult.Success("Disponibilidades recuperadas con éxito", listaDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al recuperar disponibilidad: {ex.Message}");
                return OperationResult.Failure($"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        // Obtener todas las disponibilidades
        public async Task<OperationResult> GetAllasync()
        {
            try
            {
                _logger.LogInformation("Intentando recuperar todas las disponibilidades");

                var resultado = await _repository.GetAllasync();

                if (!resultado.IsSuccess)
                {
                    _logger.LogError($"Error al recuperar disponibilidades");
                    return resultado;
                }

                var listaDisponibilidad = resultado.Data as IEnumerable<MDisponibilidad>;
                var listaMapeo = listaDisponibilidad.Select(MapeoDTO).ToList();

                return OperationResult.Success("Disponibilidades recuperadas con exito", listaMapeo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al recuperar disponibilidades: {ex.Message}");
                return OperationResult.Failure($"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        //Mapeo
        private MDisponibilidad MapeoEntidad(DisponibilidadDTO disponibilidadDTO)
        {
            if (disponibilidadDTO == null)
            {
                return null;
            }

            return new MDisponibilidad
            {
                ID_Estilista = disponibilidadDTO.ID_EstilistaDTO,
                DiaSemana = disponibilidadDTO.DiaSemanaDTO,
                HoraInicio = disponibilidadDTO.HoraInicioDTO,
                HoraFin = disponibilidadDTO.HoraFinDTO
            };
        }

        private DisponibilidadDTO MapeoDTO(MDisponibilidad MDisponibilidad)
        {
            if (MDisponibilidad == null)
            {
                return null;
            }

            return new DisponibilidadDTO
            {
                ID_EstilistaDTO = MDisponibilidad.ID_Estilista,
                DiaSemanaDTO = MDisponibilidad.DiaSemana,
                HoraInicioDTO = MDisponibilidad.HoraInicio,
                HoraFinDTO = MDisponibilidad.HoraFin
            };
        }



    }
}
