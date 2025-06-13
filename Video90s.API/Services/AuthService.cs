using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Video90s.API.Data;
using Video90s.API.Models;
using Video90s.API.Dtos;

namespace Video90s.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _cfg;
        private readonly PasswordHasher<User> _hasher;

        public AuthService(ApplicationDbContext db, IConfiguration cfg)
        {
            _db     = db;
            _cfg    = cfg;
            _hasher = new PasswordHasher<User>();
        }

        public async Task<bool> RegisterAsync(UserRegisterDto dto)
        {
            // EF Core extension AnyAsync()
            if (await _db.Users.AnyAsync(u => u.UserName == dto.UserName))
                return false;

            var user = new User
            {
                UserName     = dto.UserName,
                Email        = dto.Email,
                RegisteredAt = DateTime.UtcNow,
                IsActive     = true,
                Role         = string.IsNullOrWhiteSpace(dto.Role) ? "User" : dto.Role
            };
            user.PasswordHash = _hasher.HashPassword(user, dto.Password);

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<string> LoginAsync(UserLoginDto dto)
        {
            // EF Core SingleOrDefaultAsync()
            var user = await _db.Users.SingleOrDefaultAsync(u => u.UserName == dto.UserName);
            if (user == null) return null;

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,           user.UserName),
                new Claim(ClaimTypes.Role,           user.Role)
            };

            var key   = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_cfg["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer:              _cfg["Jwt:Issuer"],
                audience:            _cfg["Jwt:Audience"],
                claims:              claims,
                expires:             DateTime.UtcNow.AddHours(2),
                signingCredentials:  creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
