using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LocalServicesDirectory.Application.Interfaces;
using LocalServicesDirectory.Application.Dtos;

namespace LocalServicesDirectory.DirectoryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _service;

        public ServicesController(IServiceService service)
        {
            _service = service;
        }

       
        [HttpGet]
        public async Task<IActionResult> Search(
            [FromQuery] string? q,
            [FromQuery] Guid? categoryId,
            [FromQuery] Guid? cityId,
            [FromQuery] int minRating = 0,
            [FromQuery] int skip = 0,
            [FromQuery] int take = 50)
        {
            if (!string.IsNullOrWhiteSpace(q))
                return Ok(await _service.SearchAsync(q, skip, take));

            if (categoryId.HasValue && cityId.HasValue)
                return Ok(await _service.SearchAsync(categoryId.Value, cityId.Value, minRating, skip, take));

            
            return Ok(Array.Empty<object>());
        }

        
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dto = await _service.GetAsync(id);
            return dto is null ? NotFound() : Ok(dto);
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ServiceDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
