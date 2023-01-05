using Confluent.Kafka;
using HP.Application;
using HP.Application.EventHandlers;
using HP.Core.Events;
using HP.Core.Models;
using HP.Domain;
using HP.Infrastructure;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.EventHandlers;
using HP.Infrastructure.Kafka;
using HP.Infrastructure.Repository;
using MediatR;
using MongoDB.Bson.Serialization;

var builder = WebApplication.CreateBuilder(args);
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
if (env == "Development")
    builder.Configuration.AddJsonFile("appsettings.Development.json", optional: false, true).AddEnvironmentVariables();
else
    builder.Configuration.AddJsonFile("appsettings.json", optional: false, true).AddEnvironmentVariables();


BsonClassMap.RegisterClassMap<DomainEvent>();
BsonClassMap.RegisterClassMap<PersonDomainEvents.PersonCreated>();
BsonClassMap.RegisterClassMap<TodoDomainEvents.TodoCreated>();
BsonClassMap.RegisterClassMap<TodoDomainEvents.TodoUpdated>();


builder.Services.AddScoped<IMongoDbContext, MongoDbContext>();
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ITodoEventHandler, TodoEventHandlers>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventConsumer, EventConsumer>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IInMemoryBus, InMemoryBus>();
builder.Services.AddMediatR(typeof(DemoLibMediatREntryPoint).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
