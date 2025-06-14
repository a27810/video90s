using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Video90s.API.Services;

namespace Video90s.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExternalController : ControllerBase
    {
        private readonly IExternalService _external;
        public ExternalController(IExternalService external) => _external = external;

        /// <summary>
        /// GET api/external/shows/{title}
        /// Busca shows por título en TVMaze.
        /// </summary>
        [HttpGet("shows/{title}")]
        public async Task<IActionResult> GetShows(string title)
        {
            var shows = await _external.SearchShowsAsync(title);
            return Ok(shows);
        }
    }
}
