using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using cbtBackend.Dtos.RequestModels;
using cbtBackend.Dtos.ResponseModels;
using cbtBackend.Repositories.Interfaces;
using cbtBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
namespace cbtBackend.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UserController : ControllerBase
    {
        ILoginService _loginService;
        IUserRepository _userRepository;
        public UserController(ILoginService loginService,IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponse<LoginRequestModel>>> Login([FromBody] LoginRequestModel model)
        {
            var userCreds = await _loginService.LoginAsync(model);
            if (userCreds.Status == false)
            {
                return Unauthorized(userCreds);
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, userCreds.Data!.Role),
                new Claim(ClaimTypes.Name, userCreds.Data.UserName),
                new Claim(ClaimTypes.Email, userCreds.Data.Email),
                new Claim(ClaimTypes.NameIdentifier, userCreds.Data.Id)
            };
            string token = GenerateToken(claims);
            return Ok(new { Token = token , User = userCreds});
        }
    //    [HttpGet("Logout")]
    //     public async Task<IActionResult> Logout()
    //     {

    //     }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            byte[] keyBytes = new byte[32]; // 256-bit key
            RandomNumberGenerator.Fill(keyBytes);
            string secureKey = Convert.ToBase64String(keyBytes);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "http://localhost:5120",
                audience: "http://localhost:5120",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}