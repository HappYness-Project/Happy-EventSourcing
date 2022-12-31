using Confluent.Kafka;
using HP.Core.Events;
using HP.Core.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace HP.Infrastructure.Kafka
{
    public class EventProducer : IEventProducer
    {
        private readonly ProducerConfig _config;

        public EventProducer(IOptions<ProducerConfig> config)
        {
            _config = config.Value;
            var testConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };
        }
        public async Task ProducerAsync<T>(string topic, T @event) where T : IDomainEvent
        {
            using var producer = new ProducerBuilder<string, string>(_config)
                .SetKeySerializer(Serializers.Utf8)
                .SetValueSerializer(Serializers.Utf8).Build();

            var eventMsg = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = JsonSerializer.Serialize(@event, @event.GetType())
            };

            var deliveryResult = await producer.ProduceAsync(topic, eventMsg);
            if (deliveryResult.Status == PersistenceStatus.NotPersisted)
            {
                throw new Exception($"Could not produce {@event.GetType().Name} message to topic - {topic} due to the following reason : {deliveryResult.Message}.");
            }
        }
    }
}
