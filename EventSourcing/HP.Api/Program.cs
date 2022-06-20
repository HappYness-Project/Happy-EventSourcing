using HP.Application;
using HP.Application.Queries.Person;
using HP.Domain;
using HP.Domain.Person;
using HP.Domain.Todos;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
builder.Services.AddSingleton<IEventStore, EventStoreRepository>();
// make use of the same session accross different handler classes that are part of the flow.
builder.Services.AddTransient<IPersonRepository, PersonRepository>();
builder.Services.AddTransient<ITodoRepository, TodoRepository>(); 
builder.Services.AddMediatR(typeof(DemoLibMediatREntryPoint).Assembly);
builder.Services.AddMediatR(typeof(GetPersonByIdQuery));
builder.Services.AddMediatR(typeof(GetPersonByNameQuery));
builder.Services.AddMediatR(typeof(GetPersonListQuery));
builder.Configuration.AddJsonFile("appsettings.json", optional: false, true).AddEnvironmentVariables();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
