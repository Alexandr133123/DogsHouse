using Microsoft.AspNetCore.Mvc;

namespace DogsHouse.Api.Controllers
{
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        [Route("/ping")]
        public IActionResult Ping()
        {
            return Ok("DogsHouseService.Version1.0.1");
        }
    }
}
