using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Video90s.API.Data;
using Video90s.API.Models;

namespace Video90s.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public RentalsController(ApplicationDbContext db) => _db = db;

        // GET api/rentals
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Incluimos información del usuario y la película
            var list = await _db.Rentals
                                .Include(r => r.User)
                                .Include(r => r.Movie)
                                .ToListAsync();
            return Ok(list);
        }

        // GET api/rentals/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var r = await _db.Rentals
                             .Include(x => x.User)
                             .Include(x => x.Movie)
                             .SingleOrDefaultAsync(x => x.Id == id);
            if (r == null) return NotFound();
            return Ok(r);
        }
    }
}
