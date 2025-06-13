using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
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

        // GET api/movies?genre=&available=&sortBy=price|releaseDate&sortDir=asc|desc
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] string? genre,
            [FromQuery] bool? available,
            [FromQuery] string? sortBy,
            [FromQuery] string sortDir = "asc"
        )
        {
            // 1) Base query
            var q = _db.Movies.AsQueryable();

            // 2) Filtros
            if (!string.IsNullOrWhiteSpace(genre))
                q = q.Where(m => m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));

            if (available.HasValue)
                q = q.Where(m => m.IsAvailable == available.Value);

            // 3) Ordenación
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                bool desc = sortDir.Equals("desc", StringComparison.OrdinalIgnoreCase);
                switch (sortBy.Trim().ToLower())
                {
                    case "price":
                        q = desc
                            ? q.OrderByDescending(m => m.Price)
                            : q.OrderBy(m => m.Price);
                        break;

                    case "releasedate":
                        q = desc
                            ? q.OrderByDescending(m => m.ReleaseDate)
                            : q.OrderBy(m => m.ReleaseDate);
                        break;

                    default:
                        // si no coincide, dejamos el orden natural (por PK)
                        break;
                }
            }

            // 4) Ejecutar y devolver
            var list = await q.ToListAsync();
            return Ok(list);
        }

        // GET api/movies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var m = await _db.Movies.FindAsync(id);
            if (m == null) return NotFound();
            return Ok(m);
        }

        // POST api/movies
        [HttpPost]
        public async Task<IActionResult> Post(Movie m)
        {
            _db.Movies.Add(m);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = m.Id }, m);
        }

        // PUT api/movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Movie m)
        {
            if (id != m.Id) return BadRequest();
            _db.Entry(m).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/movies/5
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
