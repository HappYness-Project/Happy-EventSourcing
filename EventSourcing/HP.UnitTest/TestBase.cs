using AutoMapper;
using Confluent.Kafka;
using HP.Application.Mappers;
using HP.Core.Events;
using HP.Domain;
using HP.Infrastructure;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.EventHandlers;
using HP.Infrastructure.Kafka;
using HP.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.test
{
    public class TestBase
    {
        protected IConfiguration _configuration;
        protected IMongoDbContext _mongoDbContext;
        protected IMapper _mapper;
        protected IOptions<ProducerConfig> _producerConfig;
        protected IOptions<ConsumerConfig> _consumerConfig;
        protected IEventStoreRepository _esRepository;
        protected IEventProducer _eventProducer;
        protected IEventConsumer _eventConsumer;
        protected ITodoEventHandler _eventHandler;
        protected IEventStore _eventStore;
        [SetUp]
        public async Task BeforeTestStart()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile(@"appsettings.json", optional:false, true).Build();
            _mongoDbContext = new MongoDbContext(_configuration);
            if(_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _esRepository = new EventStoreRepository(_mongoDbContext);
            _eventProducer = new EventProducer(_producerConfig);
            _eventConsumer = new EventConsumer(_consumerConfig,_eventHandler);
            _eventStore = new EventStore(_esRepository, _eventProducer);
        }

        [Test]
        public void DbCotextReturnCollectionTodo()
        {
        }
    }
}
