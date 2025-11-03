using JBF.Application.Base;
using JBF.Domain.Base;
using ReservaCitasBackend.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Application.Interfaces
{
    public interface ICitasRepository : IRepositoryBase<MCitas>
    {
        public Task<OperationResult> DeleteAsync(int id);
    }
}
