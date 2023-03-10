using Identity.Data;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Identity.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager= userManager;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("register")]
        public IActionResult RegisterGet()
        {
            return View("Register");
        }
        [HttpPost("register")]
        public IActionResult RegisterPost([FromBody]CreateUser request)
        {
            var alice = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true,
            };
            var result = _userManager.CreateAsync(alice, request.Password).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = _userManager.AddClaimsAsync(alice, new Claim[]{
                            new Claim(ClaimTypes.GivenName, request.FirstName),
                            new Claim(ClaimTypes.Surname, request.LastName),
                            new Claim(ClaimTypes.Email, request.Email)
                        }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            return Ok(result);
        }

        [HttpGet("login")]
        public IActionResult LoginGet()
        {
            return View("login");
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginPost(CreateUser request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            return Ok(user);
        }
    }
}
