using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController()
        {

        }
        [HttpGet("welcome")]
        public async Task<IActionResult> Hehehe()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://host.docker.internal:7075/WeatherForecast");
            return Ok(response);
        }
    }
}
