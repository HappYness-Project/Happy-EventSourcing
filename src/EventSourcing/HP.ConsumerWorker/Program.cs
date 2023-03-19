using Confluent.Kafka;
using HP.ConsumerWorker;
using HP.Core.Common;
using HP.Core.Events;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.EventHandlers;
using HP.Infrastructure.Kafka;
using HP.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((ctx, builder) => 
    {
         builder.AddJsonFile("appsettings.Development.json", optional: false, true).AddEnvironmentVariables();
    })
    .ConfigureServices((hostContext,services) =>
    {
        var connstr_postgres  = hostContext.Configuration.GetConnectionString("postgres");
        var getConfig = hostContext.Configuration;


        services.AddScoped<IEventConsumer, EventConsumer>();
        services.AddDbContextFactory<HpReadDbContext> (opt =>
        {
            opt.EnableSensitiveDataLogging();
            opt.UseNpgsql(connstr_postgres);
        });
        // services.AddEntityFrameworkNpgsql().AddDbContext<HpReadDbContext>(opt =>
        // {
        //     opt.EnableSensitiveDataLogging();
        //     opt.UseNpgsql(connstr_postgres);
        // });
        services.Configure<ConsumerConfig>(getConfig.GetSection(nameof(ConsumerConfig)));
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<ITodoEventHandler, TodoEventHandler>();
        services.AddScoped<IPersonEventHandler, PersonEventHandlers>();
        services.AddHostedService<Worker>();


    })
    .Build();

host.Run();
