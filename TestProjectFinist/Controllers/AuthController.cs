using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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
        public ActionResult<LoginReply> Login(string number, string password)
        {
            try
            {
                var result = _authClient.Login(new LoginRequest { Number = number, Password = password });
                if (result.ClientName=="")
                {
                    throw new Exception("Пользователь не найден");
                }
                return result;
            }
            catch (Exception ex)
            {
                var errorResponse = new { message = ex.Message};
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}