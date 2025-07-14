using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using test1.Data;
using test1.Models;

namespace test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserLoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto login)
        {
            // Kullanıcı adı ve şifre ile giriş kontrolü
            var user = _context.Users.FirstOrDefault(u => u.userName == login.UserName && u.password == login.Password);
            if (user == null)
            {
                return Unauthorized("Geçersiz kullanıcı adı veya şifre.");
            }

            // Token claim'leri
            var claims = new[]
            {
                new Claim("UserID", user.userID.ToString()),
                new Claim("UserName", user.userName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecretkeyforyourjwt256bitlength!!"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7151",
                audience: "https://localhost:7151",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                Token = tokenString,
                User = user
            });
        }
    }
}
