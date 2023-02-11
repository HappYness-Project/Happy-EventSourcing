using BlazorUI;
using BlazorUI.Data;
using BlazorUI.Services;
using BlazorUI.Services.ItemEdit;
using BlazorUI.Services.Person;
using BlazorUI.Services.Todo;
using HP.Application;
using HP.Core.Common;
using HP.Core.Events;
using HP.Domain;
using HP.Domain.People.Write;
using HP.Domain.Todos.Write;
using HP.Infrastructure;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.EventHandlers;
using HP.Infrastructure.Kafka;
using HP.Infrastructure.Repository;
using HP.Infrastructure.Repository.Write;
using HP.Shared.Contacts;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
//builder.Services.AddSingleton<IUserManager, UserManagerFake>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient<IUserManager, UserManager>();
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpClient<ITodoService, TodoService>();
builder.Services.AddHttpClient<IPersonService, PersonService>();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, true).AddEnvironmentVariables();

var app = builder.Build();
var currentUserService = app.Services.GetRequiredService<ICurrentUserService>();
TestData.CreateTestUser();
currentUserService.CurrentUser = BlazorUI.Data.TestData.TestUser;


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
