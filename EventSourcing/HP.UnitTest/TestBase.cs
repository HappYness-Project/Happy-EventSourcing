﻿using AutoMapper;
using Confluent.Kafka;
using HP.Application.Mappers;
using HP.Core.Events;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.EventHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace HP.test
{
    public abstract class TestBase
    {
        protected IConfiguration _configuration;
        protected IMongoDbContext _mongoDbContext;
        protected IMapper _mapper;
        protected IOptions<ProducerConfig> _producerConfig;
        protected IOptions<ConsumerConfig> _consumerConfig;
        protected Mock<IEventProducer> _eventProducer;
        protected Mock<IEventConsumer> _eventConsumer;
        protected ITodoEventHandler _todoEventHandler;
        protected IPersonEventHandler _personEventHandler;
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
            _eventProducer = new();
            _eventConsumer = new();

        }
    }
}
