using Confluent.Kafka;
using HP.Core.Events;
using HP.Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace HP.Infrastructure.Kafka
{
    public class EventConsumer : IEventConsumer
    {
        private readonly ConsumerConfig _config;
        private readonly ILogger<EventConsumer> _logger;

        public EventConsumer(IOptions<ConsumerConfig> config,ILogger<EventConsumer> logger)
        {
            _logger = logger;
            _config = config.Value;
            var testConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };
        }
        public void Consumer(string topic)
        {
            throw new NotImplementedException();
        }
    }
}
