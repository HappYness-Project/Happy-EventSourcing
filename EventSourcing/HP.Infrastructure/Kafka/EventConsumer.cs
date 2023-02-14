using Confluent.Kafka;
using HP.Core.Events;
using HP.Core.Models;
using HP.Domain;
using HP.Infrastructure.EventHandlers;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
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
                OnEventReceived(@event);
                consumer.Commit(consumerResult);
            }
        }
        protected virtual Task OnEventReceived(IDomainEvent @event)
        {
            if (@event.EventType.Contains("Person"))
            {
                var handleMethod = _personEventHandler.GetType().GetMethod("On", new Type[] { @event.GetType() });
                if(handleMethod == null)
                    throw new ArgumentNullException(nameof(handleMethod), "Could not find evente handler method!");

                handleMethod.Invoke(_personEventHandler, new object[] { @event });

            }
            else if(@event.EventType.Contains("Todo"))
            {
                var handleMethod = _todoEventHandler.GetType().GetMethod("On", new Type[] { @event.GetType() });
                if (handleMethod == null)
                    throw new ArgumentNullException(nameof(handleMethod), "Could not find evente handler method!");

                handleMethod.Invoke(_todoEventHandler, new object[] { @event });
            }
            else
            {
            }
            return Task.FromResult(true);
        }
    }
}
