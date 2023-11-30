using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly Auth.AuthClient _authClient;

        public AuthController(ILogger<AuthController> logger, Auth.AuthClient authClient)
        {
            _logger = logger;
            _authClient = authClient;
        }

        [HttpPost]
        public ActionResult<string> Login(string number, string password)
        {
            try
            {
                LoginReply result = _authClient.Login(new LoginRequest { Number = number, Password = password });
                if (result.IsExist == false)
                {
                    throw new Exception("Пользователь не найден");
                }
                var token = GenerateJwtToken(number);

                return token;
            }
            catch (Exception ex)
            {
                var errorResponse = new { message = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        private string GenerateJwtToken(string phoneNumber)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.MobilePhone, phoneNumber)
            };

            var jwt = new JwtSecurityToken(
            issuer: "MyAuthServer",
                    audience: "MyAuthClient",
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication")), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}