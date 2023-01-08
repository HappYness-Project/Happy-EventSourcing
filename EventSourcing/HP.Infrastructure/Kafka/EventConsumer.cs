using Confluent.Kafka;
using HP.Core.Events;
using HP.Core.Models;
using HP.Infrastructure.EventHandlers;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace HP.Infrastructure.Kafka
{
    public class EventConsumer : IEventConsumer
    {
        private readonly ConsumerConfig _config;
        private readonly ITodoEventHandler _eventHandler;
        public EventConsumer(IOptions<ConsumerConfig> config, ITodoEventHandler eventHandler)
        {
            _config = config.Value;
            _eventHandler = eventHandler;
        }
        public void Consumer(string topic)
        {
            using var consumer = new ConsumerBuilder<string, string>(_config)
                .SetKeyDeserializer(Deserializers.Utf8)
                .SetValueDeserializer(Deserializers.Utf8)
                .Build();

            consumer.Subscribe(topic);

            while (true)
            {
                var consumerResult = consumer.Consume();
                if (consumerResult?.Message == null) continue;

                var options = new JsonSerializerOptions { Converters = { new EventJsonConverter() } };
                var @event = JsonSerializer.Deserialize<IDomainEvent>(consumerResult.Message.Value, options);
                //var handleMethod = _eventHandler.GetType().GetMethod("On", new Type[] { @event.GetType() });
                //if (handleMethod == null)
                //    throw new ArgumentNullException(nameof(handleMethod), "Could not find evente handler method!");
                //handleMethod.Invoke(_eventHandler, new object[] { @event });
                consumer.Commit(consumerResult);
            }
        }
    }
}
