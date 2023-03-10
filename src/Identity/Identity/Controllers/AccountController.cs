using Identity.Data;
using Identity.Helper;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using System.Globalization;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identity.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IOpenIddictApplicationManager _appManager;
        private IOpenIddictScopeManager _scopeManager;
        private OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> _test;
        public AccountController(UserManager<ApplicationUser> userManager, IOpenIddictScopeManager scopeManager, IOpenIddictApplicationManager appManager)
        {
            _userManager = userManager;
            _scopeManager = scopeManager;
            _appManager = appManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("register/user")]
        public IActionResult RegisterGet()
        {
            return View("Register");
        }

        [HttpPost("register/user")]
        public async Task<IActionResult> RegisterUser(CreateUser request)
        {
            //create user, use AspNetCore.identity
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                EmailConfirmed = true,
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = _userManager.AddClaimsAsync(alice, new Claim[]{
                            new Claim(ClaimTypes.Name, "Alice Smith"),
                            new Claim(ClaimTypes.GivenName, "Alice"),
                            new Claim(ClaimTypes.Surname, "Smith"),
                            new Claim(ClaimTypes.Webpage, "http://alice.com"),
                            new Claim(ClaimTypes.StreetAddress, "72 Pinnacle Drive"),
                            new Claim(ClaimTypes.StateOrProvince, "Ontario"),
                            new Claim(ClaimTypes.PostalCode, "N2P 1C5"),
                        }).Result;
                            new Claim(ClaimTypes.StateOrProvince, "Ontario"),
                            new Claim(ClaimTypes.PostalCode, "N2P 1C5"),
                        }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            return Ok(result);
        }

        [HttpGet("update")]
        public IActionResult UpdateUser()
        {
            //get user
            //get application
            return View("Update");
        }
        /*[HttpPost("update")]
        public async Task<IActionResult> UpdateUser(UpdatesUser request)
        {
            //update user
            var user = await _userManager.FindByIdAsync(request.Id);
            var existingClaims = await _userManager.GetClaimsAsync(user);
            await _userManager.UpdateAsync(user);
            foreach(var claim in existingClaims)
            {
                if (claim.Type == ClaimTypes.Email)
                    await _userManager.ReplaceClaimAsync(claim, new Claim(ClaimTypes.Email, request.Email));
            }


        }*/



        [HttpGet("register/api")]
        public IActionResult RegisterApi()
        {
            return View("Api");
        }
        [HttpPost("register/api")]
        public async Task<IActionResult> RegisterApi(RegisterApp request)
        {
            var clientId = request.ApplicationName + ".randomly.generated.clientId.api";
            var clientPassword = "hashedPassword";
            if (await _appManager.FindByClientIdAsync(clientId) is null)
            {
                await _appManager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = clientId,
                    ClientSecret = clientPassword,
                    DisplayName = request.ApplicationName,
                    Permissions =
                        {
                            Permissions.Endpoints.Introspection
                        }
                });
                if (await _scopeManager.FindByNameAsync(request.ApplicationName) is null)
                {
                    await _scopeManager.CreateAsync(new OpenIddictScopeDescriptor
                    {
                        DisplayName = request.ApplicationName + " API",
                        Name = request.ApplicationName,
                        Resources =
                            {
                                clientId// clientId of OpenIddictApplicationDescriptor
                            }
                    });
                    return Ok(new RegisterAppResult
                    {
                        ClientId = clientId,
                        ClientPassword = clientPassword
                    });
                }
            }
            return Ok("Already registered");

        }


        [HttpGet("register/application")]
        public async Task<IActionResult> RegisterApp()
        {
            List<(string,string)> apis = new List<(string, string)>();
            /*var apps = _appManager.ListAsync(2);
            await foreach(var app in apps)
            {
                apis.Add(await _appManager.GetDisplayNameAsync(app));
            }*/
            var scopes = _scopeManager.ListAsync(2);
            await foreach (var app in scopes)
            {
                apis.Add((await _scopeManager.GetDisplayNameAsync(app), await _scopeManager.GetNameAsync(app)));
            }
            ViewBag.Scopes = apis;
            return View("App");
        }
        [HttpPost("register/application")]
        public async Task<IActionResult> RegisterApp(RegisterApp request)
        {
            var clientId = request.ApplicationName + ".randomly.generated.clientId.app";
            var clientPassword = "hashedPassword";

            if (await _appManager.FindByClientIdAsync(clientId) is null)
            {
                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = clientId,
                    ClientSecret = clientPassword,
                    DisplayName = request.ApplicationName,
                    ConsentType = ConsentTypes.Explicit,
                    RedirectUris =
                        {
                            new Uri(request.CallbakUri)
                        },
                    Permissions =
                        {
                            Permissions.Endpoints.Introspection,
                            Permissions.Endpoints.Authorization,
                            Permissions.Endpoints.Logout,
                            Permissions.Endpoints.Token,
                            Permissions.GrantTypes.AuthorizationCode,
                            Permissions.ResponseTypes.Code,
                            Permissions.Scopes.Email,
                            Permissions.Scopes.Profile,
                            Permissions.Scopes.Roles,

                        },
                    Requirements =
                        {
                            Requirements.Features.ProofKeyForCodeExchange
                        }
                };

                foreach(var permission in request.SelectedApis)
                {
                    descriptor.Permissions.Add(Permissions.Prefixes.Scope + permission);
                }

                await _appManager.CreateAsync(descriptor);

                return Ok(new RegisterAppResult
                {
                    ClientId = clientId,
                    ClientPassword = clientPassword
                });
            };
            return Ok("Already registered");
            
        }

        /*[HttpGet("login")]
        public IActionResult LoginGet()
        {
            return View("login");
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginPost(CreateUser request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var result = await _signInManager.PasswordSignInAsync(user,request.Password,false,false);
            if (result.Succeeded)
            {

            }
            return Ok(result);
        }*/
    }
}
