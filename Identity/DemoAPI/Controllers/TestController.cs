using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Hehehe()
        {
            /*var client = new HttpClient();
            var response = await client.GetAsync("https://localhost:8001/api/test/welcome");*/
            return Ok(1);
        }
    }
}
