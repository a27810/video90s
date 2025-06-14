using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Video90s.API.Data;
using Video90s.API.Models;

namespace Video90s.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public UsersController(ApplicationDbContext db) => _db = db;

        // GET api/users
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _db.Users.ToListAsync();
            return Ok(list);
        }

        // GET api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var u = await _db.Users.FindAsync(id);
            if (u == null) return NotFound();
            return Ok(u);
        }
    }
}
