using JBF.Application.DTOs;
using JBF.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JBF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _loginService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogin loginService, ILogger<LoginController> logger)
        {
            _loginService = loginService;
            _logger = logger;
        }

        // POST: api/login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null || string.IsNullOrWhiteSpace(loginDTO.Correo) || string.IsNullOrWhiteSpace(loginDTO.Password))
            {
                _logger.LogWarning("Datos de login nulos o incompletos");
                return BadRequest("Correo y contraseña son obligatorios");
            }

            var result = await _loginService.Loginasync(loginDTO);

            if (!result.IsSuccess)
            {
                _logger.LogWarning($"Error al iniciar sesion: {result.Message}");
                return Unauthorized(result.Message);
            }

            return Ok(result);
        }
    }
}
