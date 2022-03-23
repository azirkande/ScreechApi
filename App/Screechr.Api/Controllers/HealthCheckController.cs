using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Screechr.Api.Controllers
{
    [Route("HealthCheck")]
    [ApiController]
    [AllowAnonymous]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "healthy";
        }
    }
}
