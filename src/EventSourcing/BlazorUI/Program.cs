using BlazorUI.Data;
using BlazorUI.Services;
using BlazorUI.Services.Person;
using BlazorUI.Services.Todo;
using HP.Shared.Contacts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
//builder.Services.AddSingleton<IUserManager, UserManagerFake>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpClient<IUserManager, UserManager>();
builder.Services.AddHttpClient<ITodoService, TodoService>();
builder.Services.AddHttpClient<IPersonService, PersonService>();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, true).AddEnvironmentVariables();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddOpenIdConnect(options =>
    {
        options.SignInScheme = "Cookies";
        options.Authority = "https://localhost:7281";
        options.ClientId = "hp_service_client";
        options.ClientSecret = "hp_service_secret";
        options.RequireHttpsMetadata = true;
        options.ResponseType = OpenIdConnectResponseType.Code;
        options.Scope.Add("profile");
        options.Scope.Add("api1");
        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.Events = new OpenIdConnectEvents
        {
            OnAccessDenied = context =>
            {
                context.HandleResponse();
                context.Response.Redirect("/");
                return Task.CompletedTask;
            }
        };
    });


var app = builder.Build();
var currentUserService = app.Services.GetRequiredService<ICurrentUserService>();
TestData.CreateTestUser();
currentUserService.CurrentUser = BlazorUI.Data.TestData.TestUser;


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await app.RunAsync();
