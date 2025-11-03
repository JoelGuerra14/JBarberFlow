using Domain.Entities;
using JBF.Application.Interfaces;
using JBF.Domain.Base;
using JBF.Persistence.Base;
using JBF.Persistence.BD;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Persistence.Repositories
{
    public class EstilistaRepository : RepositoryBase<MEstilista>, IEstilistasRepository
    {
        //Objetos y contextos de la bd
        private readonly Context _context;
        private readonly ILogger<EstilistaRepository> _logger;

        //Constructor
        public EstilistaRepository(Context context, ILogger<EstilistaRepository> logger) : base(context)
        {
            this._context = context;
            this._logger = logger;
        }

        public async override Task<OperationResult> GetbyIdasync(int id)
        {
            try
            {
                _logger.LogInformation($"Recuperando estilista...");

                var traerEstilista = await base.GetbyIdasync(id);

                if (traerEstilista.IsSuccess)
                {
                    _logger.LogError($"Error al recuperar perfil de estilista con id {id}");
                }

                return OperationResult.Success($"Estilista con id {id} recuperado con exito", traerEstilista.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al recuperar estilista: {ex.Message}");
                return OperationResult.Failure($"Ocurrio un error al recuperar estilista con id {id}: {ex.Message}");
            }
        }

        public async override Task<OperationResult> GetAllasync()
        {
            try
            {
                _logger.LogInformation($"Recuperando todos los estilista...");

                var traerEstilistas = await base.GetAllasync();

                if (!traerEstilistas.IsSuccess)
                {
                    _logger.LogError("Error al traer todos los perfiles de estilistas");
                }

                //AGREGAR EL SOFT DELETE
                //_logger.LogInformation($"Se recuperaron {} registros de estilistas");
                return OperationResult.Success("Estilistas recuperados con exito", traerEstilistas.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al recuperar todos los estilistas: {ex.Message}");
                return OperationResult.Failure($"Ocurrio un error al recuperar todos los estilistas: {ex.Message}");
            }
        }

        public async override Task<OperationResult> Createasync(MEstilista estilista)
        {
            try
            {
                _logger.LogInformation($"Creando estilistas...");

                var resultado = await base.Createasync(estilista);

                if (!resultado.IsSuccess)
                {
                    return resultado;
                }
                _logger.LogInformation("Perfil de estilista creado");
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al guardar los datos: {ex.Message}");
                return OperationResult.Failure($"Ocurrio un error al actualizar los datos: {ex.Message}");
            }
        }

        public async override Task<OperationResult> Updateasync(MEstilista estilista)
        {
            try
            {
                _logger.LogInformation($"Actualizando estilistas...");

                var resultado = await base.Updateasync(estilista);
                if (!resultado.IsSuccess)
                {
                    return resultado;
                }

                _logger.LogInformation("Perfil de estilista actualizado");
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
