using JBF.Application.DTOs;
using JBF.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JBF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstilistaController : ControllerBase
    {
        private readonly IEstilista _estilistaService;
        private readonly ILogger<EstilistaController> _logger;

        public EstilistaController(IEstilista estilistaService, ILogger<EstilistaController> logger)
        {
            _estilistaService = estilistaService;
            _logger = logger;
        }

        //GET: api/estilistas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _estilistaService.GetAllasync();

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        //GET: api/estilista/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _estilistaService.GetbyIdasync(id);

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result.Data);
        }

        //POST: api/estilista
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EstilistaDTO dto)
        {
            if (dto == null)
                return BadRequest("Los datos enviados son invalidos");

            var result = await _estilistaService.Createasync(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        //PUT: api/estilista/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] EstilistaDTO dto)
        {
            if (dto == null)
                return BadRequest("Los datos enviados son invalidos");

            var result = await _estilistaService.Updateasync(id, dto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
    }
}
