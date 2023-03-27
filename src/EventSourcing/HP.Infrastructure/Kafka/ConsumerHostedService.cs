using HP.Core.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace HP.Infrastructure.Kafka
{
    public class ConsumerHostedService : IHostedService
    {
        private readonly ILogger<ConsumerHostedService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly string _topicName;
        public ConsumerHostedService(ILogger<ConsumerHostedService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _topicName = _serviceProvider.GetRequiredService<IConfiguration>()["KafkaTopicName"];
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Event Consumer service running.");
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();
                Task.Run(() => eventConsumer.Consumer(_topicName), cancellationToken);
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
