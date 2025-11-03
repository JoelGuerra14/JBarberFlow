using JBF.Application.DTOs;
using JBF.Application.Interfaces;
using JBF.Domain.Base;
using JBF.Persistence.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Application.Services
{
    public class LoginService : ILogin
    {
        private readonly ILoginRepository _repository;
        private readonly ILogger<LoginService> _logger;

        public LoginService(ILoginRepository repository, ILogger<LoginService> logger) 
        { 
          this._repository = repository;
          this._logger = logger;
        }

        async Task<OperationResult> ILogin.Loginasync(LoginDTO loginDTO)
        {
            try
            {
                _logger.LogInformation("Intentando iniciar sesion");

                var login = await _repository.LoginAsync(loginDTO.Correo, loginDTO.Password);

                if (!login.IsSuccess)
                {
                    _logger.LogError("Error al recuperar al usuario");
                    return login;
                }

                if (login == null)
                {
                    _logger.LogError("Usuario no existente");
                    return login;
                }

                _logger.LogInformation($"Sesion iniciada");
                return OperationResult.Success("Sesion iniciada");
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error inesperado al iniciar sesion: {ex.Message}");
                return OperationResult.Failure($"Error inesperado al iniciar sesion: {ex.Message}");
            }
        }
    }
}
