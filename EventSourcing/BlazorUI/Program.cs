using BlazorUI.Services;
using BlazorUI.Services.ItemEdit;
using HP.Application;
using HP.Domain;
using HP.Infrastructure;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using HP.Shared.Contacts;
using HP.UnitTest;
//using HP.UnitTest;
//using HP.UnitTest.UserManager;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//builder.Services.AddSingleton<IUserManager, UserManagerFake>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddSingleton<ItemEditService>();

builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
builder.Services.AddSingleton<IEventStore, EventStore>();
builder.Services.AddTransient<IPersonRepository, PersonRepository>();
builder.Services.AddTransient<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<IInMemoryBus, InMemoryBus>();
builder.Services.AddMediatR(typeof(DemoLibMediatREntryPoint).Assembly);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, true).AddEnvironmentVariables();


var app = builder.Build();
var currentUserService = app.Services.GetRequiredService<ICurrentUserService>();
TestData.CreateTestUser();
currentUserService.CurrentUser = TestData.TestUser;


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await app.RunAsync();
