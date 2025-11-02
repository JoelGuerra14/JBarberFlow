using Microsoft.AspNetCore.Mvc;
using JBF.Application.Dtos;
using JBF.Application.Interfaces;
using JBF.Domain.Base;
using JBF.Application.DTOs;

namespace JBF.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly ICitaService _citaService;
        private readonly ILogger<CitasController> _logger;

        public CitasController(ICitaService citaService, ILogger<CitasController> logger)
        {
            _citaService = citaService;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(List<CitaDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> GetAllCitas()
        {
            var result = await _citaService.GetAllCitasAsync();
            
            if (!result.IsSuccess)
            {
                return HandleServiceFailure(result, "GetAllCitas");
            }
            
            return Ok(result.Data);
        }

        [HttpGet("Get{id}")]
        [ProducesResponseType(typeof(CitaDto), 200)]
        [ProducesResponseType(typeof(OperationResult), 404)]
        public async Task<IActionResult> GetCitaById(int id)
        {
            var result = await _citaService.GetCitaByIdAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }
            
            return Ok(result.Data);
        }

        [HttpPost("CreateCita")]
        [ProducesResponseType(typeof(CitaDto), 201)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        public async Task<IActionResult> CreateCita([FromBody] CreateCitaDto createCitaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _citaService.CreateCitaAsync(createCitaDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            var citaDto = (CitaDto)result.Data;
            
            return CreatedAtAction(nameof(GetCitaById), new { id = citaDto.ID_Citas }, citaDto);
        }

        [HttpPut("UpdateCita{id}")]
        [ProducesResponseType(typeof(CitaDto), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 404)]
        public async Task<IActionResult> UpdateCita(int id, [FromBody] UpdateCitaDto updateCitaDto)
        {
            if (id != updateCitaDto.ID_Citas)
            {
                return BadRequest(OperationResult.Failure("El ID de la ruta no coincide con el ID de la solicitud."));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _citaService.UpdateCitaAsync(updateCitaDto);

            if (!result.IsSuccess)
            {
                if (result.Message.Contains("no encontrada"))
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }

            return Ok(result.Data);
        }

        [HttpDelete("DeleteCita{id}")]
        [ProducesResponseType(typeof(OperationResult), 200)]
        [ProducesResponseType(typeof(OperationResult), 404)]
        public async Task<IActionResult> CancelCita(int id)
        {
            var result = await _citaService.CancelCitaAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        private IActionResult HandleServiceFailure(OperationResult result, string originMethod)
        {
            _logger.LogError("Error en {Method}: {ErrorMessage}. Detalles: {ErrorData}", 
                originMethod, 
                result.Message, 
                (object)result.Data!);
                
            return StatusCode(500, result);
        }
    }
}