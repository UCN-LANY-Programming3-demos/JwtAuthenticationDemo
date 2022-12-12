using AuthenticationService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserProvider;

namespace AuthenticationService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserContext _userContext;

        public AuthenticationController(IConfiguration config, IUserContext userContext)
        {
            _config = config;
            _userContext = userContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return Ok("Provide user information to log in...");
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            // validate password
            if (login.IsValid && _userContext.ValidateLogin(login.Username, login.Password, out UserInfo? userInfo))
            {
                // login accepted                 
                var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim("username", login.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = _config["Jwt:Issuer"],
                    Audience = _config["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return Ok(stringToken);
            }
            return Unauthorized();
        }

        [HttpGet("userinfo")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            string username = User.Claims.First(c => c.Type == "username").Value;
            UserInfo? info = _userContext.GetUserInfo(username);
            return Ok(info);
        }
    }
}
