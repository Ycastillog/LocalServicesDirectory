using LocalServicesDirectory.Infrastructure.Persistence;
using LocalServicesDirectory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocalServicesDirectory.DirectoryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly LocalServicesContext _db;
        public CitiesController(LocalServicesContext db) => _db = db;

        // GET: api/cities
        [HttpGet]
        public Task<List<City>> Get() => _db.Cities.AsNoTracking().ToListAsync();

        // GET: api/cities/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<City>> Get(Guid id)
            => await _db.Cities.FindAsync(id) is { } c ? c : NotFound();

        
        [HttpPost]
        public async Task<ActionResult<City>> Post([FromBody] City city)
        {
            _db.Cities.Add(city);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = city.Id }, city);
        }

        
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] City city)
        {
            if (id != city.Id) return BadRequest();
            _db.Entry(city).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var city = await _db.Cities.FindAsync(id);
            if (city is null) return NotFound();

            _db.Cities.Remove(city);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

