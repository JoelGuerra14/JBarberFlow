using JBF.Application.DTOs;
using JBF.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JBF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisponibilidadController : ControllerBase
    {
        private readonly IDisponibilidad _disponibilidadService;
        private readonly ILogger<DisponibilidadController> _logger;

        public DisponibilidadController(IDisponibilidad disponibilidadService, ILogger<DisponibilidadController> logger)
        {
            _disponibilidadService = disponibilidadService;
            _logger = logger;
        }

        // GET: api/disponibilidad
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _disponibilidadService.GetAllasync();

            if (!result.IsSuccess)
            {
                _logger.LogWarning($"Error al obtener disponibilidades: {result.Message}");
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // GET: api/disponibilidad/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _disponibilidadService.GetbyIdasync(id);

            if (!result.IsSuccess)
            {
                _logger.LogWarning($"Disponibilidad con ID {id} no encontrada: {result.Message}");
                return NotFound(result.Message);
            }

            return Ok(result);
        }

        // POST: api/disponibilidad
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DisponibilidadDTO disponibilidadDTO)
        {
            if (disponibilidadDTO == null)
            {
                _logger.LogWarning("Datos de disponibilidad nulos al intentar crear registro");
                return BadRequest("Los datos enviados no son validos");
            }

            var result = await _disponibilidadService.Createasync(disponibilidadDTO);

            if (!result.IsSuccess)
            {
                _logger.LogWarning($"Error al crear disponibilidad: {result.Message}");
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // PUT: api/disponibilidad/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] DisponibilidadDTO disponibilidadDTO)
        {
            if (disponibilidadDTO == null)
            {
                _logger.LogWarning("Datos de disponibilidad nulos al intentar actualizar registro");
                return BadRequest("Los datos enviados no son validos");
            }

            var result = await _disponibilidadService.Updateasync(id, disponibilidadDTO);

            if (!result.IsSuccess)
            {
                _logger.LogWarning($"Error al actualizar disponibilidad con ID {id}: {result.Message}");
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
