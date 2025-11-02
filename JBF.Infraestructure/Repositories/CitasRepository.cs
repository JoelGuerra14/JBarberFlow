using JBF.Application.Interfaces;
using JBF.Domain.Base;
using JBF.Persistence.BD;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservaCitasBackend.Modelos;
using System.Linq.Expressions;

namespace JBF.Persistence.Repositories
{
    public class CitasRepository : ICitasRepository
    {
        private readonly Context _context;
        private readonly ILogger<CitasRepository> _logger;

        public CitasRepository(Context context, ILogger<CitasRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<OperationResult> Createasync(MCitas entity)
        {
            try
            {
                bool overlap = await ExistsAsync(c =>
                    c.ID_Estilista == entity.ID_Estilista &&
                    !c.IsCanceled &&
                    entity.FechaInicio < c.FechaFin &&
                    entity.FechaFin > c.FechaInicio);

                if (overlap)
                {
                    _logger.LogWarning("Intento de crear cita con superposición de horario para Estilista ID {EstilistaId}", entity.ID_Estilista);
                    return OperationResult.Failure("El estilista ya tiene una cita programada en ese horario.");
                }

                await _context.Citas.AddAsync(entity);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Cita creada exitosamente con ID {CitaId}", entity.ID_Citas);
                return OperationResult.Success("Cita creada exitosamente.", entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la cita.");
                return OperationResult.Failure("Error al crear la cita.", ex);
            }
        }

        public async Task<OperationResult> GetAllasync()
        {
            _logger.LogInformation("Iniciando GetAllasync para MCitas...");
            try
            {
                var citas = await _context.Citas
                    .Where(c => !c.IsCanceled) 
                    .ToListAsync();

                _logger.LogInformation("Se obtuvieron {Count} citas.", citas.Count);
                return OperationResult.Success("Citas obtenidas exitosamente.", citas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las citas.");
                return OperationResult.Failure("Error al obtener las citas.", ex);
            }
        }

        public async Task<OperationResult> GetbyIdasync(int id)
        {
            _logger.LogInformation("Iniciando GetbyIdasync para MCitas con ID {CitaId}", id);
            try
            {
                var cita = await _context.Citas
                    .FirstOrDefaultAsync(c => c.ID_Citas == id);

                if (cita == null)
                {
                    _logger.LogWarning("No se encontró la cita con ID {CitaId}", id);
                    return OperationResult.Failure("Cita no encontrada.");
                }

                _logger.LogInformation("Cita con ID {CitaId} encontrada.", id);
                return OperationResult.Success("Cita encontrada.", cita);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar la cita con ID {CitaId}", id);
                return OperationResult.Failure("Error al buscar la cita.", ex);
            }
        }

        public async Task<OperationResult> Updateasync(MCitas entity)
        {
            _logger.LogInformation("Iniciando Updateasync para MCitas con ID {CitaId}", entity.ID_Citas);
            try
            {
                var existingCita = await _context.Citas.FindAsync(entity.ID_Citas);

                if (existingCita == null)
                {
                    _logger.LogWarning("No se encontró la cita con ID {CitaId} para actualizar.", entity.ID_Citas);
                    return OperationResult.Failure("Cita no encontrada para actualizar.");
                }
                _context.Entry(existingCita).CurrentValues.SetValues(entity);
                _context.Entry(existingCita).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Cita con ID {CitaId} actualizada exitosamente.", entity.ID_Citas);
                return OperationResult.Success("Cita actualizada exitosamente.", existingCita);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning(ex, "Conflicto de concurrencia al actualizar Cita ID {CitaId}", entity.ID_Citas);
                return OperationResult.Failure("Error de concurrencia. Los datos fueron modificados por otro usuario.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la cita ID {CitaId}", entity.ID_Citas);
                return OperationResult.Failure("Error al actualizar la cita.", ex);
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<MCitas, bool>> filter)
        {
            _logger.LogDebug("Iniciando ExistsAsync para MCitas...");
            try
            {
                return await _context.Citas.AnyAsync(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en ExistsAsync al verificar la cita.");
                return false;
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            try
            {
                var cita = await _context.Citas.FindAsync(id);
                if (cita == null)
                {
                    _logger.LogWarning("No se encontró la cita con ID {CitaId} para cancelar (Soft Delete).", id);
                    return OperationResult.Failure("Cita no encontrada.");
                }

                if (cita.IsCanceled)
                {
                    _logger.LogInformation("La cita con ID {CitaId} ya se encontraba cancelada.", id);
                    return OperationResult.Success("La cita ya estaba cancelada.", cita);
                }
                cita.IsCanceled = true;
                _context.Entry(cita).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                _logger.LogInformation("Cita con ID {CitaId} cancelada exitosamente (Soft Delete).", id);
                return OperationResult.Success("Cita cancelada exitosamente.", cita);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cancelar (Soft Delete) la cita ID {CitaId}", id);
                return OperationResult.Failure("Error al cancelar la cita.", ex);
            }
        }
    }
}
