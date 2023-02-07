using AutoMapper;
using Confluent.Kafka;
using HP.Application.Mappers;
using HP.Core.Common;
using HP.Core.Events;
using HP.Core.Models;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.EventHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HP.test
{
    public abstract class TestBase
    {
        protected IConfiguration _configuration;
        protected IOptions<ProducerConfig> _producerConfig;
        protected IOptions<ConsumerConfig> _consumerConfig;
        protected Mock<IMapper> _mapperMock;
        protected Mock<IEventProducer> _eventProducer;
        protected Mock<IEventConsumer> _eventConsumer;
        protected Mock<IMongoDbContext> _mongoDbContext;

        [SetUp]
        public async Task BeforeTestStart()
        {

            _eventProducer = new();
            _eventConsumer = new();
            _mongoDbContext = new();
            _mapperMock = new();

        }


        public static T AssertPubblishedDoaminEvent<T>(AggregateRoot aggregate) where T : IDomainEvent
        {
            var domainEvent = DomainEventsTestHelper.GetAllDomainEvents(aggregate).OfType<T>().SingleOrDefault();
            if (domainEvent == null)
            {
                throw new Exception($"{typeof(T).Name} event not published");
            }
            return domainEvent;
        }
    }
}
