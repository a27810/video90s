using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Video90s.API.Data;
using Video90s.API.Models;

namespace Video90s.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public MoviesController(ApplicationDbContext db) => _db = db;

        // GET api/movies?genre=&available=&sort=price
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string genre,
                                             [FromQuery] bool? available,
                                             [FromQuery] string sort)
        {
            var q = _db.Movies.AsQueryable();
            if (!string.IsNullOrEmpty(genre))
                q = q.Where(m => m.Genre == genre);
            if (available.HasValue)
                q = q.Where(m => m.IsAvailable == available.Value);
            if (sort == "price")
                q = q.OrderBy(m => m.Price);

            return Ok(await q.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var m = await _db.Movies.FindAsync(id);
            if (m == null) return NotFound();
            return Ok(m);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Movie m)
        {
            _db.Movies.Add(m);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = m.Id }, m);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Movie m)
        {
            if (id != m.Id) return BadRequest();
            _db.Entry(m).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var m = await _db.Movies.FindAsync(id);
            if (m == null) return NotFound();
            _db.Movies.Remove(m);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
