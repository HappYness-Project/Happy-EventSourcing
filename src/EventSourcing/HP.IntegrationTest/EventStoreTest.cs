using Confluent.Kafka;
using EventStore.Client;
using FluentAssertions;
using HP.Core.Models;
using HP.Core.Test;

namespace HP.IntegrationTest
{
    public class EventStoreTest : TestBase
    {

        [SetUp]
        public void Init()
        {
        }

        [Test]
        public async Task EmptyEvents_SaveEventsCalled_ThenNothingHappen()
        {
            await _eventStore.SaveEventsAsync(Guid.NewGuid(), "NewDummyType", null, 0);
        }

        [Test]
        public async Task CreateEvents_SaveEventsCalled_Then()
        {
            var guid = Guid.NewGuid();
            string nameOfStream = "DummyAggregate" + guid;
            DummyCreatedEvent dummyCreatedEvent = new DummyCreatedEvent { DummyName = "Dummyname", DummyType = "TestingType"};
            List<DomainEvent> events = new List<DomainEvent> { dummyCreatedEvent };

            await _eventStore.SaveEventsAsync(guid, nameOfStream, events, 0);

            //await _eventStore.GetEventsAsync(guid, )
        }



        [Test]
        public async Task GivenInvalidStream_EventsShouldBeZero()
        {
            var result = await _eventStore.GetEventsAsync(Guid.NewGuid(), "NotExistingStream");
            
            result.Count().Should().Be(0);
        }

    }
}
