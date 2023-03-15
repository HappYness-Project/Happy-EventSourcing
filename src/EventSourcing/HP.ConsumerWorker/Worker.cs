using HP.Core.Events;
using HP.Infrastructure.DbAccess;
using Microsoft.EntityFrameworkCore;

namespace HP.ConsumerWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private IDbContextFactory<HpReadDbContext> myDbContextFactory;
    private readonly IServiceScopeFactory _scopeFactory;
    private string _topicName;
    public Worker(IConfiguration config, ILogger<Worker> logger,IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _topicName = config["KafkaTopicName"];
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Event Consumer Worker running at: {time}", DateTimeOffset.Now);
            using (var scope = _scopeFactory.CreateScope())
            {
                var consumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();
                await consumer.Consumer(_topicName);
            }
            await Task.Delay(1000, stoppingToken);
            
        }
    }
}
