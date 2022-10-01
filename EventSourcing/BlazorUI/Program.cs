using BlazorUI.Data;
using HP.Application;
using HP.Domain;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
builder.Services.AddSingleton<IEventStore, EventStoreRepository>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<IPersonRepository, PersonRepository>();
builder.Services.AddTransient<ITodoRepository, TodoRepository>();

builder.Services.AddMediatR(typeof(DemoLibMediatREntryPoint).Assembly);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, true).AddEnvironmentVariables();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
