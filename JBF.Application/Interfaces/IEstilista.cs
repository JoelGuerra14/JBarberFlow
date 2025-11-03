using JBF.Application.DTOs;
using JBF.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Application.Interfaces
{
    public interface IEstilista
    {
        public Task<OperationResult> GetbyIdasync(int id);
        public Task<OperationResult> GetAllasync();
        public Task<OperationResult> Createasync(EstilistaDTO estilistaDTO);
        public Task<OperationResult> Updateasync(int id, EstilistaDTO estilistaDTO);
    }
}
