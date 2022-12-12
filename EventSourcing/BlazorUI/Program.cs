using BlazorUI;
using BlazorUI.Data;
using BlazorUI.Services;
using BlazorUI.Services.ItemEdit;
using BlazorUI.Services.Person;
using BlazorUI.Services.Todo;
using HP.Application;
using HP.Core.Events;
using HP.Domain;
using HP.Infrastructure;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using HP.Shared.Contacts;
//using HP.UnitTest;
//using HP.UnitTest.UserManager;
using MediatR;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddSingleton<IUserManager, UserManagerFake>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddSingleton<ItemEditService>();

builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
builder.Services.AddSingleton<IEventStore, EventStore>();
builder.Services.AddTransient<IPersonRepository, PersonRepository>();
builder.Services.AddTransient<ITodoRepository, TodoRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddHttpClient<ITodoService, TodoService>();
builder.Services.AddHttpClient<IPersonService, PersonService>();
builder.Services.AddScoped<IInMemoryBus, InMemoryBus>();
builder.Services.AddMediatR(typeof(DemoLibMediatREntryPoint).Assembly);
builder.Services.AddScoped<TodoState>();
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
