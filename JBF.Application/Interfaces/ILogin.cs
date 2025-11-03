using JBF.Application.Base;
using JBF.Application.DTOs;
using JBF.Domain.Base;
using ReservaCitasBackend.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Persistence.Repositories
{
    public interface ILogin
    {
       public Task<OperationResult> Loginasync(LoginDTO loginDTO);
    }
}
