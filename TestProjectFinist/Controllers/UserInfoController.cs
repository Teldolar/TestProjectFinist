using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserInfoController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly UserInfo.UserInfoClient _userInfoClient;

        public UserInfoController(ILogger<AuthController> logger, UserInfo.UserInfoClient userInfoClient)
        {
            _logger = logger;
            _userInfoClient = userInfoClient;
        }

        [HttpGet]
        public ActionResult<UserInfoReply> GetUserInfo()
        {
            try
            {
                string jwtToken = HttpContext.Request.Headers["Authorization"]!;
                jwtToken = jwtToken.Replace("Bearer ", "");
                UserInfoReply result = _userInfoClient.GetUserInfo(new UserInfoRequest { Number = DecodeJwt(jwtToken)});
                return result;
            }
            catch (Exception ex)
            {
                var errorResponse = new { message = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        private static string DecodeJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            string mobilePhoneClaim = jsonToken!.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.MobilePhone)!.Value;

            return mobilePhoneClaim;
        }
    }
}