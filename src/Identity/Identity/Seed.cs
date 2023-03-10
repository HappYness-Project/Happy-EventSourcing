using Identity.Data;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using Serilog;
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
    public class Seed : IHostedService
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
                        DisplayName = "DemoAPI",
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
                        ClientSecret = "codeflow_pkce_client_secret",
                        ConsentType = ConsentTypes.Explicit,
                        DisplayName = "Blazor code PKCE",
                        PostLogoutRedirectUris =
                        {
                            new Uri("https://localhost:7220/signout-callback-oidc")
                        },
                        RedirectUris =
                        {
                            new Uri("https://localhost:7220/signin-oidc")
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
                            Permissions.Prefixes.Scope + "api1"
                        },
                        Requirements =
                        {
                            Requirements.Features.ProofKeyForCodeExchange
                        }
                    });
                }

                if(await manager.FindByClientIdAsync("postmanClient") is null)
                {
                    await manager.CreateAsync(new OpenIddictApplicationDescriptor
                    {
                        ClientId = "postmanClient",
                        ClientSecret = "postman_secret",
                        ConsentType = ConsentTypes.Explicit,
                        DisplayName = "Postman as client",
                        RedirectUris =
                        {
                            new Uri("https://oauth.pstmn.io/v1/callback")
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
                            Permissions.Prefixes.Scope + "api1"
                        },
                        Requirements =
                        {
                            Requirements.Features.ProofKeyForCodeExchange
                        }
                    });
                }
                if (await manager.FindByClientIdAsync("hp_service_client") is null)
                {
                    await manager.CreateAsync(new OpenIddictApplicationDescriptor
                    {
                        ClientId = "hp_service_client",
                        ClientSecret = "hp_service_secret",
                        ConsentType = ConsentTypes.Explicit,
                        DisplayName = "hp service Client",
                        RedirectUris =
                        {
                            new Uri("https://oauth.pstmn.io/v1/callback")
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
                if (await userMgr.FindByNameAsync("HpAdmin@email.com") is null)
                {
                    var bob = new ApplicationUser
                    {
                        UserName = "HpAdmin@email.com",
                        Email = "HpAdmin@email.com",
                        EmailConfirmed = true
                    };
                    var result = await userMgr.CreateAsync(bob, "Happy#1234!");
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = await userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(ClaimTypes.Name, "Happy Ness"),
                            new Claim(ClaimTypes.GivenName, "Happy"),
                            new Claim(ClaimTypes.Surname, "Ness"),
                            new Claim(ClaimTypes.Email, "HpAdmin@gmail.com"),
                            new Claim("location", "somewhere"),
                            new Claim(Claims.Picture, "imge-url")
                    });
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Log.Debug("Happyness admin created");
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
