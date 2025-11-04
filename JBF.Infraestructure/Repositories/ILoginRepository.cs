using JBF.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Persistence.Repositories
{
    public interface ILoginRepository
    {
        Task<OperationResult> LoginAsync(string email, string password);
    }
}
