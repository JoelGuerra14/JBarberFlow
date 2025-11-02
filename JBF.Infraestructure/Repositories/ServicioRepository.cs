using Domain.Entities;
using JBF.Application.Interfaces;
using JBF.Domain.Base;
using JBF.Persistence.Base;
using JBF.Persistence.BD;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JBF.Persistence.Repositories
{
    public class ServicioRepository : RepositoryBase<MServicio>, IServicioRepository
    {
        private readonly Context _context;
        private readonly ILogger<MServicio> _logger;
        public ServicioRepository(Context context, ILogger<MServicio> logger) : base(context)
        {
            this._context = context;
            this._logger = logger;
        }

        public override async Task<OperationResult> GetbyIdasync(int id)
        {
            _logger.LogInformation($"Intentando recuperar Servicio con ID: {id}");
            var result = await base.GetbyIdasync(id);

            if (!result.IsSuccess)
            {
                _logger.LogError($"Error al recuperar Servicio ID {id}: {result.Message}");
            }

            return result;
        }

        public override async Task<OperationResult> GetAllasync()
        {
            try
            {
                var entidades = await _context.Servicios
                                              .Where(s => !s.IsDeleted)
                                              .AsNoTracking()
                                              .ToListAsync();

                _logger.LogInformation($"Se recuperaron {entidades.Count} servicios activos.");
                return OperationResult.Success("Servicios obtenidos con éxito.", entidades);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al recuperar todos los Servicios: {ex.Message}", ex);
                return OperationResult.Failure($"Ocurrió un error al obtener los servicios: {ex.Message}");
            }
        }

        public override async Task<OperationResult> Createasync(MServicio entity)
        {
            if (entity == null)
            {
                _logger.LogError("Se intentó crear un Servicio nulo.");
                return OperationResult.Failure("La entidad no puede ser nula.");
            }

            _logger.LogInformation($"Intentando crear Servicio: {entity.Nombre}");

            try
            {
                var result = await base.Createasync(entity);

                if (!result.IsSuccess)
                {
                    _logger.LogError($"Error al crear Servicio: {result.Message}");
                    return result;
                }

                _logger.LogInformation($"Servicio '{entity.Nombre}' creado con éxito (ID: {entity.ID_Servicio}).");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al crear Servicio '{entity.Nombre}': {ex.Message}", ex);
                return OperationResult.Failure($"Error inesperado: {ex.Message}");
            }
        }

        public override async Task<OperationResult> Updateasync(MServicio entity)
        {
            if (entity == null)
            {
                _logger.LogError("Se intentó actualizar un Servicio nulo.");
                return OperationResult.Failure("La entidad no puede ser nula.");
            }

            _logger.LogInformation($"Intentando actualizar Servicio ID: {entity.ID_Servicio}");

            try
            {
                var result = await base.Updateasync(entity);

                if (!result.IsSuccess)
                {
                    _logger.LogError($"Error al actualizar Servicio ID {entity.ID_Servicio}: {result.Message}");
                    return result;
                }

                _logger.LogInformation($"Servicio ID {entity.ID_Servicio} actualizado con éxito.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al actualizar Servicio ID {entity.ID_Servicio}: {ex.Message}", ex);
                return OperationResult.Failure($"Error inesperado: {ex.Message}");
            }
        }

        public async Task<OperationResult> Deleteasync(int id)
        {
            _logger.LogInformation($"Intentando (soft) delete de Servicio ID: {id}");

            try
            {
                var entidad = await _context.Servicios.FindAsync(id);

                if (entidad == null)
                {
                    _logger.LogWarning($"Servicio ID {id} no encontrado para eliminar.");
                    return OperationResult.Failure($"Servicio con ID {id} no encontrado.");
                }

                if (entidad.IsDeleted)
                {
                    _logger.LogWarning($"Servicio ID {id} ya estaba eliminado.");
                    return OperationResult.Failure($"Servicio con ID {id} no encontrado.");
                }
                entidad.IsDeleted = true;

                _context.Entry(entidad).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                _logger.LogInformation($"Servicio ID {id} marcado como eliminado (IsDeleted=true).");
                return OperationResult.Success("Servicio marcado como eliminado con éxito.", entidad);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al hacer soft delete de Servicio ID {id}: {ex.Message}", ex);
                return OperationResult.Failure($"Ocurrió un error al eliminar el servicio: {ex.Message}");
            }
        }
    }
}
