using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using JBF.Domain.Base;

namespace JBF.Application.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<OperationResult> GetbyIdasync(int id);
        Task<OperationResult> GetAllasync();
        Task<OperationResult> Createasync(TEntity entity);
        Task<OperationResult> Updateasync(TEntity entity);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);

    }
}