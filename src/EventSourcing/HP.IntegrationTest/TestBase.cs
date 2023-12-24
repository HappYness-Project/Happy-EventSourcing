using AutoMapper;
using Confluent.Kafka;
using EventStore.Client;
using HP.Application.Mappers;
using HP.Core.Events;
using HP.Infrastructure.EventHandlers;
using HP.Infrastructure.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
namespace HP.IntegrationTest
{
    public abstract class TestBase
    {
        protected IConfiguration _configuration;
        protected IMapper _mapper;
        protected IEventStore _eventStore;
        protected IEventProducer _eventProducer;
        protected IOptions<ProducerConfig> _producerConfig;
        protected ITodoEventHandler _todoEventHandler;
        protected IPersonEventHandler _personEventHandler;
        protected EventStoreClient _eventStoreClient;
        [SetUp]
        public async Task BeforeTestStart()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile(@"appsettings.json", optional: false, true).Build();



            var iconfigSec = _configuration.GetSection(nameof(ProducerConfig));
            var pc = new ProducerConfig { BootstrapServers = iconfigSec["BootstrapServers"] };
            IOptions<ProducerConfig> appSettingsOptions = Options.Create(pc);
            _producerConfig = appSettingsOptions;
            _eventProducer = new EventProducer(_producerConfig, "HP");
           
            var settings = EventStoreClientSettings.Create("esdb://localhost:2113?tls=false");
            _eventStoreClient = new EventStoreClient(settings);
            _eventStore = new Infrastructure.EventStore(_eventStoreClient, _eventProducer);
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }


        }
    }
}
