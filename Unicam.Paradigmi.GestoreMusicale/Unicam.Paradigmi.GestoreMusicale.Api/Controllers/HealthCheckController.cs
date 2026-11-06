using Microsoft.AspNetCore.Mvc;

namespace Unicam.Paradigmi.GestoreMusicale.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class HealthCheckController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }
    }
}
