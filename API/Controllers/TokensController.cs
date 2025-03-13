using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/tokens")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TokensController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generate a token
        /// </summary>
        /// <param name="account">Account data</param>
        /// <returns>token</returns>
        [HttpPost("generate")]
        public string GenerateJwtToken([FromBody] Account account)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            // Tạo khóa bí mật từ secret key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Tạo các claims từ thông tin người dùng
            var claims = new List<Claim>()
            {
                new Claim("code", account.AccountCode)
            };

            // Tạo token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Solve a token
        /// </summary>
        /// <param name="token">jwt</param>
        /// <returns>Chars</returns>
        [HttpPost("solve")]
        public IActionResult SolveToken([FromBody] string token)
        {
            // Bộ giải mã JWT
            var handler = new JwtSecurityTokenHandler();

            // Kiểm tra xem token có hợp lệ không
            if (!handler.CanReadToken(token)) { return Problem(); }
            // Giải mã token
            var jwtToken = handler.ReadJwtToken(token);

            // Lấy payload từ token
            var claims = jwtToken.Claims;

            var code = claims.Where(x => x.Type == "code").First();
            if(code!.Value == null) { return NotFound(); }

            return Ok(code.Value);
        }
    }
}
