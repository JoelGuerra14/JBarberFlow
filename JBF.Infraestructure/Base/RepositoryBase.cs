using JBF.Application.Base;
using JBF.Domain.Base;
using JBF.Persistence.BD;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Persistence.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly Context _context;
        private DbSet<TEntity> Entity { get; set; }
        public RepositoryBase(Context context)
        {
            _context = context;
            Entity = _context.Set<TEntity>();
        }

        public virtual async Task<OperationResult> GetbyIdasync(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var entity = await Entity.FindAsync(id);

                if (entity != null)
                {
                    return OperationResult.Success("Entidad recuperada", entity);
                }
                else
                {
                    return OperationResult.Failure($"Entidad con el ID {id} no encontrada");
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"Un error ocurrio mientras se recuperaban los datos con el ID {id}: {ex.Message}");
            }
        }

        public virtual async Task<OperationResult> GetAllasync()
        {
            try
            {
                var entities = await Entity.ToListAsync();

                return OperationResult.Success("Entidades recuperada", entities);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"Ocurrio un error al recuperar todas las entidades: {ex.Message}");
            }
        }

        public virtual async Task<OperationResult> Createasync(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                await Entity.AddAsync(entity);
                await _context.SaveChangesAsync();
                result.IsSuccess = true;
                result.Message = "Entidad creada";
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Ha ocurrido un error al guardar los datos: {ex.Message}";
            }
            return result;
        }

        public virtual async Task<OperationResult> Updateasync(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                Entity.Update(entity);
                await _context.SaveChangesAsync();
                result.IsSuccess = true;
                result.Message = "Entidad actualizada";
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Ocurrio un error al actualizar los datos: {ex.Message}";
            }
            return result;
        }
        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Entity.AnyAsync(filter);
        }
    }
}
