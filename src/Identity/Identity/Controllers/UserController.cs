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
        private SignInManager<ApplicationUser> _signInManager;  
        private IOpenIddictApplicationManager _appManager;
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOpenIddictApplicationManager appManager)
        {
            _userManager = userManager;
            _appManager = appManager;
            _signInManager = signInManager;
        }

        // API로 user 등록하기
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(CreateUser request)
        {
            var alice = new ApplicationUser
            {
                UserName = request.Email,// AddDefaultUI() 에서는 UserNmae이 Email과 같아야하는 것 같다..
                Email = request.Email,
                EmailConfirmed = true,
            };
            var result = await _userManager.CreateAsync(alice, request.Password);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = await _userManager.AddClaimsAsync(alice, new Claim[]{
                            new Claim(ClaimTypes.Name, request.UserName),
                            new Claim(ClaimTypes.GivenName, request.FirstName),
                            new Claim(ClaimTypes.Surname, request.LastName),
                            new Claim(ClaimTypes.Email, request.Email),
                        });
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, false);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
