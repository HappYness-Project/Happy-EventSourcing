using Confluent.Kafka;
using HP.Core.Events;
using HP.Infrastructure.EventHandlers;
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
        public static IServiceCollection AddKafkaEventConsumer(this IServiceCollection services, string topicName)
        {
            return services.AddSingleton<IEventConsumer>(c =>
            {
                var logger = c.GetRequiredService<ILogger<EventConsumer>>();
                var getConfig = c.GetRequiredService<IOptions<ConsumerConfig>>();
                var eventHandler = c.GetRequiredService<ITodoEventHandler>();
                return new EventConsumer(getConfig, eventHandler);
            });
        }
    }
}
