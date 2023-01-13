using Confluent.Kafka;
using HP.Application;
using HP.Application.EventHandlers;
using HP.Core.Common;
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
var getConfig = builder.Configuration;
builder.Services.Configure<ProducerConfig>(getConfig.GetSection(nameof(ProducerConfig)));
builder.Services.Configure<ConsumerConfig>(getConfig.GetSection(nameof(ConsumerConfig)));
builder.Services.AddScoped<IPersonAggregateRepository, PersonRepository>();
builder.Services.AddScoped<ITodoAggregateRepository, TodoRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<ITodoEventHandler, TodoEventHandlers>();
builder.Services.AddScoped<IPersonEventHandler, HP.Infrastructure.EventHandlers.PersonEventHandlers>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddKafkaEventProducer(getConfig["KafkaTopicName"]);
//builder.Services.AddKafkaEventConsumer(getConfig["KafkaTopicName"]);
builder.Services.AddScoped<IEventConsumer, EventConsumer>();

builder.Services.AddScoped<IInMemoryBus, InMemoryBus>();
builder.Services.AddMediatR(typeof(DemoLibMediatREntryPoint).Assembly);

builder.Services.AddControllers();
builder.Services.AddHostedService<ConsumerHostedService>();

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
