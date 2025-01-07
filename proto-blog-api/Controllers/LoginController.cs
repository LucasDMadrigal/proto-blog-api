using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using proto_blog_api.DTOs;
using proto_blog_api.Models;
using proto_blog_api.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace proto_blog_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private StoreContext _context;
        private IConfiguration _configuration;

        public LoginController(StoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        [HttpPost]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.username);

            if (user == null || !PasswordUtils.VerifyPassword(loginDto.password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized(new { Message = "Credenciales inválidas" });
            }

            var token = GenerateJwtToken(user);

            return Ok(new LoginResponseDto { Token = token});
        }

        //private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        //{
        //    using var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt);
        //    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //    return computedHash.SequenceEqual(storedHash);
        //}

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString()) // Asegúrate de que el rol esté definido en el modelo User
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is missing from configuration.")));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
