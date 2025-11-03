using JBF.Application.DTOs;
using JBF.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JBF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuarioService;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuario usuarioService, ILogger<UsuarioController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        // GET: api/usuario
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _usuarioService.GetAllasync();

            if (!result.IsSuccess)
            {
                _logger.LogWarning($"Error al obtener los usuarios: {result.Message}");
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // GET: api/usuario/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _usuarioService.GetbyIdasync(id);

            if (!result.IsSuccess)
            {
                _logger.LogWarning($"Usuario con ID {id} no encontrado: {result.Message}");
                return NotFound(result.Message);
            }

            return Ok(result);
        }

        // POST: api/usuario
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
            {
                _logger.LogWarning("Datos de usuario nulos al intentar crear perfil");
                return BadRequest("Los datos enviados no son validos");
            }

            var result = await _usuarioService.Createasync(usuarioDTO);

            if (!result.IsSuccess)
            {
                _logger.LogWarning($"Error al crear el usuario: {result.Message}");
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // PUT: api/usuario/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
            {
                _logger.LogWarning("Datos de usuario nulos al intentar actualizar perfil");
                return BadRequest("Los datos enviados no son validos.");
            }

            var result = await _usuarioService.Updateasync(id, usuarioDTO);

            if (!result.IsSuccess)
            {
                _logger.LogWarning($"Error al actualizar el usuario con ID {id}: {result.Message}");
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
