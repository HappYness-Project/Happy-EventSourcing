using Confluent.Kafka;
using EventStore.Client;
using FluentAssertions;
using HP.Core.Models;
using HP.Core.Test;

namespace HP.IntegrationTest
{
    [Category("Integration")]
    public class EventStoreTest : TestBase
    {
        [Test]
        public async Task CreateEvents_SaveEventsCalled_ThenEventIsUpdated()
        {
            var guid = Guid.NewGuid();
            string nameOfStream = "DummyAggregate-" + guid;
            DummyCreatedEvent dummyCreatedEvent = new DummyCreatedEvent { DummyName = "Dummyname", DummyType = "TestingType"};
            List<DomainEvent> events = new List<DomainEvent> { dummyCreatedEvent };

            await _eventStore.SaveEventsAsync(nameOfStream, events, 0);

            //await _eventStore.GetEventsAsync(guid, )
        }

        [Test]
        public async Task GivenInvalidStream_EventsShouldBeZero()
        {
            var result = await _eventStore.GetEventsAsync($"NotExistingStream-{Guid.NewGuid}");
            
            result.Count().Should().Be(0);
        }
    }
}
