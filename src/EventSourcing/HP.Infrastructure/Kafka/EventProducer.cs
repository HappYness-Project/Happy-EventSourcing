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
        private readonly string _topicName;
        public EventProducer(IOptions<ProducerConfig> config, string topicName)
        {
            _config = config.Value;
            _topicName = topicName;
        }
        public async Task ProducerAsync<T>(T @event) where T : IDomainEvent
        {
            using var producer = new ProducerBuilder<string, string>(_config)
                .SetKeySerializer(Serializers.Utf8)
                .SetValueSerializer(Serializers.Utf8).Build();

            var eventMsg = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = JsonSerializer.Serialize(@event, @event.GetType())
            };

            var deliveryResult = await producer.ProduceAsync(_topicName, eventMsg);
            if (deliveryResult.Status == PersistenceStatus.NotPersisted)
            {
                throw new Exception($"Could not produce {@event.GetType().Name} message to topic - {_topicName} due to the following reason : {deliveryResult.Message}.");
            }
        }
    }
}
