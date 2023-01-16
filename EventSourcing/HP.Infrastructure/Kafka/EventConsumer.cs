using Confluent.Kafka;
using HP.Core.Events;
using HP.Core.Models;
using HP.Infrastructure.EventHandlers;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;

namespace HP.Infrastructure.Kafka
{
    public class EventConsumer : IEventConsumer
    {
        private readonly ConsumerConfig _config;
        private readonly ITodoEventHandler _todoEventHandler;
        private readonly IPersonEventHandler _personEventHandler;
        public EventConsumer(IOptions<ConsumerConfig> config, ITodoEventHandler todoEventHandler, IPersonEventHandler personEventHandler)
        {
            _config = config.Value;
            _todoEventHandler = todoEventHandler;
            _personEventHandler = personEventHandler;
        }
        public void Consumer(string topicName)
        {
            using var consumer = new ConsumerBuilder<string, string>(_config)
                .SetKeyDeserializer(Deserializers.Utf8)
                .SetValueDeserializer(Deserializers.Utf8)
                .Build();

            consumer.Subscribe(topicName);

            while (true)
            {
                var consumerResult = consumer.Consume();
                if (consumerResult?.Message == null) 
                    continue;

                var options = new JsonSerializerOptions { Converters = { new EventJsonConverter() } };
                var @event = JsonSerializer.Deserialize<IDomainEvent>(consumerResult.Message.Value, options);
                // if( event handler is Person event handler)
                // var handleMethod = _todoEventHandler.GetType().GetMethod("On", new Type[] { @event.GetType() });

                var handleMethod = _personEventHandler.GetType().GetMethod("On", new Type[] { @event.GetType() });
                if (handleMethod == null)
                    throw new ArgumentNullException(nameof(handleMethod), "Could not find evente handler method!");

                // Should I publish from here?
                handleMethod.Invoke(_personEventHandler, new object[] { @event });
                consumer.Commit(consumerResult);  
            }
        }
    }
}
