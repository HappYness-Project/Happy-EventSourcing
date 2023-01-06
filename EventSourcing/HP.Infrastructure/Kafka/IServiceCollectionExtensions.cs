using Confluent.Kafka;
using HP.Core.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HP.Infrastructure.Kafka
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddKafkaEventProducer(this IServiceCollection services, string topicName)
        {
            return services.AddSingleton<IEventProducer>(c =>
            {
                var logger = c.GetRequiredService<ILogger<EventProducer>>();
                var getConfig = c.GetRequiredService<IOptions<ProducerConfig>>();
                return new EventProducer(getConfig, topicName);
            });
        }
    }
}
