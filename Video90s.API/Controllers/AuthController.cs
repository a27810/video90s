using Microsoft.AspNetCore.Mvc;
using Video90s.API.Dtos;
using Video90s.API.Services;

namespace Video90s.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            var ok = await _auth.RegisterAsync(dto);
            if (!ok) return BadRequest("Usuario ya existe.");
            return Created(string.Empty, "Usuario registrado.");
        }

        // POST: api/auth/login
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var token = await _auth.LoginAsync(dto);
            if (token == null) return Unauthorized("Credenciales inválidas.");
            return Ok(new { token });
        }
    }
}
