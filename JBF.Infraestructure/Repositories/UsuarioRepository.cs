using Domain.Entities;
using JBF.Application.Interfaces;
using JBF.Domain.Base;
using JBF.Persistence.Base;
using JBF.Persistence.BD;
using Microsoft.Extensions.Logging;
using ReservaCitasBackend.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Persistence.Repositories
{
    public class UsuarioRepository : RepositoryBase<MUsers>, IUsuariosRepository
    {
        //Objetos y contextos de la bd
        private readonly Context _context;
        private readonly ILogger<UsuarioRepository> _logger;

        public UsuarioRepository(Context context, ILogger<UsuarioRepository> logger) : base(context)
        {
            this._context = context;
            this._logger = logger;
        }

        public async override Task<OperationResult> GetbyIdasync(int id)
        {
            try
            {
                _logger.LogInformation($"Intentando recuperando usuario...");

                var traerUsuario = await base.GetbyIdasync(id);

                if (!traerUsuario.IsSuccess)
                {
                    _logger.LogError($"Error al enontrar perfil de usuario con {id}");
                }

                return OperationResult.Success($"usuario con id {id} recuperado con exito", traerUsuario.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al recuperar usuario: {ex.Message}");
                return OperationResult.Failure($"Ocurrio un error al recuperar usuario con id {id}: {ex.Message}");
            }
        }

        public async override Task<OperationResult> GetAllasync()
        {
            try
            {
                _logger.LogInformation($"Intentando recuperar todos los usuarios...");

                var traerUsuarios = await base.GetAllasync();

                if (!traerUsuarios.IsSuccess)
                {
                    _logger.LogError("Error al recuperar todos los usuarios");
                }

                //AGREGAR EL SOFT DELETE
                //_logger.LogInformation($"Se recuperaron {} registros de estilistas");
                return OperationResult.Success("Usuarios recuperados con exito", traerUsuarios.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al recuperar todos los usuarios: {ex.Message}");
                return OperationResult.Failure($"Ocurrio un error al recuperar todos los usuarios: {ex.Message}");
            }
        }

        public async override Task<OperationResult> Createasync(MUsers usuario)
        {
            try
            {
                _logger.LogInformation($"Intentando crear usuario...");

                var resultado = await base.Createasync(usuario);

                if (!resultado.IsSuccess)
                {
                    _logger.LogError("Error al crear perfil de usuario");
                }

                _logger.LogInformation("Perfil de usuario creado");
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al guardar los datos: {ex.Message}");
                return OperationResult.Failure($"Ocurrio un error al actualizar los datos: {ex.Message}");
            }
        }

        public async override Task<OperationResult> Updateasync(MUsers usuario)
        {
            try
            {
                _logger.LogInformation($"Intentando actualizar informacion de usuario...");

                var resultado = await base.Updateasync(usuario);
                if (!resultado.IsSuccess)
                {
                    return resultado;
                }

                _logger.LogInformation("Perfil de usuario actualizado");
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
