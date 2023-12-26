using Confluent.Kafka;
using HP.Application;
using HP.Core.Common;
using HP.Core.Events;
using HP.Core.Models;
using HP.Domain;
using HP.Domain.Todos.Write;
using HP.Infrastructure;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.EventHandlers;
using HP.Infrastructure.Kafka;
using HP.Infrastructure.Repository;
using HP.Infrastructure.Repository.Write;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;


var builder = WebApplication.CreateBuilder(args);
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
if (env == "Development")
    builder.Configuration.AddJsonFile("appsettings.Development.json", optional: false, true).AddEnvironmentVariables();
else
    builder.Configuration.AddJsonFile("appsettings.json", optional: false, true).AddEnvironmentVariables();


BsonClassMap.RegisterClassMap<DomainEvent>(cm => {
    cm.AutoMap();
    cm.SetIsRootClass(true);
});
//BsonClassMap.RegisterClassMap<DomainEvent>();
BsonClassMap.RegisterClassMap<PersonDomainEvents.PersonCreated>();
BsonClassMap.RegisterClassMap<PersonDomainEvents.PersonRemoved>();
BsonClassMap.RegisterClassMap<PersonDomainEvents.PersonInfoUpdated>();
BsonClassMap.RegisterClassMap<PersonDomainEvents.PersonRoleUpdated>();
BsonClassMap.RegisterClassMap<PersonDomainEvents.PersonGroupUpdated>();
BsonClassMap.RegisterClassMap<TodoDomainEvents.TodoCreated>();
BsonClassMap.RegisterClassMap<TodoDomainEvents.TodoUpdated>();
BsonClassMap.RegisterClassMap<TodoDomainEvents.TodoRemoved>();
BsonClassMap.RegisterClassMap<TodoDomainEvents.TodoItemCreated>();
BsonClassMap.RegisterClassMap<TodoDomainEvents.TodoItemUpdated>();
BsonClassMap.RegisterClassMap<TodoDomainEvents.TodoItemRemoved>();

var getConfig = builder.Configuration;
builder.Services.AddScoped<IMongoDbContext, MongoDbContext>();
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<HpReadDbContext>(opt =>
{
   opt.EnableSensitiveDataLogging();
   opt.UseNpgsql(getConfig.GetConnectionString("postgres"));
});

builder.Services.Configure<ProducerConfig>(getConfig.GetSection(nameof(ProducerConfig)));
builder.Services.Configure<ConsumerConfig>(getConfig.GetSection(nameof(ConsumerConfig)));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IAggregateRepository<>), typeof(AggregateRepository<>));
builder.Services.AddScoped<ITodoAggregateRepository, TodoAggregateRepository>();

builder.Services.AddScoped<ITodoEventHandler, TodoEventHandler>();
builder.Services.AddScoped<IPersonEventHandler, PersonEventHandlers>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddKafkaEventProducer(getConfig["KafkaTopicName"]);
builder.Services.AddEventStoreInfra(getConfig.GetConnectionString("eventstore"));
builder.Services.AddScoped<IEventConsumer, EventConsumer>();
builder.Services.AddScoped<IInMemoryBus, InMemoryBus>();
builder.Services.AddMediatR(typeof(DemoLibMediatREntryPoint).Assembly);

builder.Services.AddControllers();
//builder.Services.AddHostedService<ConsumerHostedService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<HpReadDbContext>();
    await context.Database.EnsureCreatedAsync();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
