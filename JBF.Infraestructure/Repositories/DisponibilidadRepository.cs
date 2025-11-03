using Domain.Entities;
using JBF.Application.Interfaces;
using JBF.Domain.Base;
using JBF.Persistence.Base;
using JBF.Persistence.BD;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Persistence.Repositories
{
    public class DisponibilidadRepository : RepositoryBase<MDisponibilidad>, IDisponibilidadRepository
    {
        private readonly Context _context;
        private readonly ILogger<DisponibilidadRepository> _logger;

        public DisponibilidadRepository(Context context, ILogger<DisponibilidadRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        //Recuperar disponibilidad de estilista en especifico segun id
        public async override Task<OperationResult> GetbyIdasync(int id)
        {
            try
            {
                _logger.LogInformation($"Recuperando disponibilidades del estilista con ID {id}...");

                var disponibilidades = await _context.Set<MDisponibilidad>()
                    .Where(d => d.ID_Estilista == id).ToListAsync();

                if (disponibilidades == null || disponibilidades.Count == 0)
                {
                    return OperationResult.Failure($"No se encontraron disponibilidades para el estilista con ID {id}");
                }

                return OperationResult.Success($"Se encontraron {disponibilidades.Count} disponibilidades para el estilista con ID {id}", disponibilidades);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al recuperar disponibilidades: {ex.Message}");
                return OperationResult.Failure($"Ocurrió un error al recuperar disponibilidades del estilista: {ex.Message}");
            }
        }

        //Traer un horario especifico
        public async Task<OperationResult> GetByDisponibilidadIdAsync(int idDisponibilidad)
        {
            try
            {
                _logger.LogInformation($"Recuperando disponibilidad con ID {idDisponibilidad}...");

                var disponibilidad = await _context.Set<MDisponibilidad>()
                    .FirstOrDefaultAsync(d => d.ID_Disponibilidad == idDisponibilidad);

                if (disponibilidad == null)
                {
                    return OperationResult.Failure($"Disponibilidad con id {idDisponibilidad} no encontrada");
                }

                return OperationResult.Success("Disponibilidad encontrada", disponibilidad);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al recuperar disponibilidad: {ex.Message}");
                return OperationResult.Failure($"Ocurrió un error al recuperar la disponibilidad: {ex.Message}");
            }
        }

        public async override Task<OperationResult> GetAllasync()
        {
            try
            {
                _logger.LogInformation($"Recuperando todos los horarios...");

                var traerHorarios= await base.GetAllasync();

                if (!traerHorarios.IsSuccess)
                {
                    _logger.LogError("Error al recuperar los horarios");
                }

                //AGREGAR EL SOFT DELETE
                //_logger.LogInformation($"Se recuperaron {} registros de estilistas");

                return OperationResult.Success("Horarios recuperados con exito", traerHorarios.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al recuperar todos los horarios: {ex.Message}");
                return OperationResult.Failure($"Ocurrio un error al recuperar todos los horarios: {ex.Message}");
            }
        }

        public async override Task<OperationResult> Createasync(MDisponibilidad disponibilidad)
        {
            try
            {
                _logger.LogInformation($"Creando horario...");

                var resultado = await base.Createasync(disponibilidad);

                if (!resultado.IsSuccess) 
                {
                    _logger.LogError("Error al crear horario");
                }

                _logger.LogInformation("Horario creado con exito");
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al guardar los datos: {ex.Message}");
                return OperationResult.Failure($"Ha ocurrido un error al guardar los datos: {ex.Message}");
            }
        }

        public async override Task<OperationResult> Updateasync(MDisponibilidad disponibilidad)
        {
            try
            {
                _logger.LogInformation($"Intentando actualizar horario...");

                var resultado = await base.Updateasync(disponibilidad);

                if (!resultado.IsSuccess)
                {
                    _logger.LogError("Error al actualizar horario");
                }

                _logger.LogInformation("Horario actualizado con exito");
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar los datos: {ex.Message}");
                return OperationResult.Failure($"Ocurrio un error al actualizar los datos: {ex.Message}");
            }
        }
    }
}