using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SecretsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SecretsController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult GetSecrets()
        {
            return Ok("This is my dirty secret...");
        }
    }
}
