using Identity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;
using System.Security.Claims;
using Identity.Models;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private IOpenIddictApplicationManager _appManager;
        public UserController(UserManager<ApplicationUser> userManager, IOpenIddictApplicationManager appManager)
        {
            _userManager = userManager;
            _appManager = appManager;
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(CreateUser request)
        {
            var alice = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                EmailConfirmed = true,
            };
            var result = await _userManager.CreateAsync(alice, request.Password);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = await _userManager.AddClaimsAsync(alice, new Claim[]{
                            new Claim(ClaimTypes.Name, request.FirstName + " " + request.LastName),
                            new Claim(ClaimTypes.GivenName, request.FirstName),
                            new Claim(ClaimTypes.Surname, request.LastName),
                            new Claim(ClaimTypes.Email, request.Email),
                        });
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            await _appManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = request.Email,
                ClientSecret = request.Password,
                ConsentType = ConsentTypes.Explicit,
            });

            return Ok(result);

        }
    }
}
