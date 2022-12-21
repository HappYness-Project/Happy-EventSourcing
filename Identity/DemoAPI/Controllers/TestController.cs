using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Hehehe()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://localhost:8001/api/test/welcome");
            return Ok(response);
        }
    }
}
