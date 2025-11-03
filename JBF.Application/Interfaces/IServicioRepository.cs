using Domain.Entities;
using JBF.Application.Base;
using JBF.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Application.Interfaces
{
    public interface IServicioRepository : IRepositoryBase<MServicio>
    {
        Task<OperationResult> Deleteasync(int id);
    }
}
