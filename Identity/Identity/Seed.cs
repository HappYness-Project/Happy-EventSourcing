using Identity.Data;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identity
{
    public class Seed:IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public Seed(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync(cancellationToken);

            await RegisterApplicationsAsync(scope.ServiceProvider);
            await RegisterScopesAsync(scope.ServiceProvider);
            await RegisterUsersAsync(scope.ServiceProvider);

            static async Task RegisterApplicationsAsync(IServiceProvider provider)
            {
                var manager = provider.GetRequiredService<IOpenIddictApplicationManager>();

                // API
                if (await manager.FindByClientIdAsync("resource_server_1") == null)
                {
                    var descriptor = new OpenIddictApplicationDescriptor
                    {
                        ClientId = "resource_server_1",
                        ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342",
                        Permissions =
                        {
                            Permissions.Endpoints.Introspection
                        }
                    };

                    await manager.CreateAsync(descriptor);
                }

                // Blazor Hosted
                if (await manager.FindByClientIdAsync("blazorcodeflowpkceclient") is null)
                {
                    await manager.CreateAsync(new OpenIddictApplicationDescriptor
                    {
                        ClientId = "blazorcodeflowpkceclient",
                        ConsentType = ConsentTypes.Explicit,
                        DisplayName = "Blazor code PKCE",
                        PostLogoutRedirectUris =
                        {
                            new Uri("https://localhost:44348/signout-callback-oidc")
                        },
                        RedirectUris =
                        {
                            new Uri("https://localhost:44348/signin-oidc")
                        },
                        ClientSecret = "codeflow_pkce_client_secret",
                        Permissions =
                        {
                            Permissions.Endpoints.Authorization,
                            Permissions.Endpoints.Logout,
                            Permissions.Endpoints.Token,
                            Permissions.GrantTypes.AuthorizationCode,
                            Permissions.ResponseTypes.Code,
                            Permissions.Scopes.Email,
                            Permissions.Scopes.Profile,
                            Permissions.Scopes.Roles,
                            Permissions.Prefixes.Scope + "api1"
                        },
                        Requirements =
                        {
                            Requirements.Features.ProofKeyForCodeExchange
                        }
                    });
                }
            }

            static async Task RegisterScopesAsync(IServiceProvider provider)
            {
                var manager = provider.GetRequiredService<IOpenIddictScopeManager>();

                if (await manager.FindByNameAsync("api1") is null)
                {
                    await manager.CreateAsync(new OpenIddictScopeDescriptor
                    {
                        DisplayName = "Dantooine API access",
                        DisplayNames =
                        {
                            [CultureInfo.GetCultureInfo("fr-FR")] = "Accès à l'API de démo"
                        },
                        Name = "api1",
                        Resources =
                        {
                            "resource_server_1"
                        }
                    });
                }
            }

            static async Task RegisterUsersAsync(IServiceProvider provider)
            {
                var userMgr = provider.GetRequiredService<UserManager<ApplicationUser>>();
                if (await userMgr.FindByNameAsync("alice") is null)
                {
                    var alice = new ApplicationUser
                    {
                        UserName = "alice",
                        Email = "AliceSmith@email.com",
                        EmailConfirmed = true,
                    };
                    var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(ClaimTypes.Name, "Alice Smith"),
                            new Claim(ClaimTypes.GivenName, "Alice"),
                            new Claim(ClaimTypes.Surname, "Smith"),
                            new Claim(ClaimTypes.Webpage, "http://alice.com"),
                            new Claim(ClaimTypes.StreetAddress, "72 Pinnacle Drive"),
                            new Claim(ClaimTypes.StateOrProvince, "Ontario"),
                            new Claim(ClaimTypes.PostalCode, "N2P 1C5"),
                        }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Log.Debug("alice created");
                }
                else
                {
                    Log.Debug("alice already exists");
                }

                if (await userMgr.FindByNameAsync("bob") is null)
                {
                    var bob = new ApplicationUser
                    {
                        UserName = "bob",
                        Email = "BobSmith@email.com",
                        EmailConfirmed = true
                    };
                    var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(ClaimTypes.Name, "Bob Smith"),
                            new Claim(ClaimTypes.GivenName, "Bob"),
                            new Claim(ClaimTypes.Surname, "Smith"),
                            new Claim(ClaimTypes.Webpage, "http://bob.com"),
                            new Claim(ClaimTypes.Email, "bob@gmail.com"),
                            new Claim(ClaimTypes.MobilePhone, "519-666-7777"),
                            new Claim("location", "somewhere")
                        }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Log.Debug("bob created");
                }
                else
                {
                    Log.Debug("bob already exists");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    }
}
