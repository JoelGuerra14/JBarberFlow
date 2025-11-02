using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JBF.Application.DTOs;
using JBF.Application.Interfaces;
using System.Net;

namespace JBF.Api.Controllers
{
    [ApiController]
    [Route("api/servicios")]
    public class ServicioController : ControllerBase
    {
        private readonly IServicioService _servicioService;

        public ServicioController(IServicioService servicioService)
        {
            _servicioService = servicioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _servicioService.GetAllAsync();
            if (!result.IsSuccess)
            {
                return NotFound(new { message = result.Message });
            }
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _servicioService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(new { message = result.Message });
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServicioDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _servicioService.CreateAsync(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Message });
            }

            var dtoCreado = result.Data as ServicioDTO;
            return CreatedAtAction(nameof(GetById), new { id = dtoCreado.ID_Servicio }, dtoCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateServicioDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _servicioService.UpdateAsync(id, dto);
            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _servicioService.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Message });
            }

            return NoContent();
        }

    }
}
