using HP.Core.Events;
using HP.Core.Models;
using HP.Domain;
using HP.Infrastructure.Repository;
using NUnit.Framework;
using System;
namespace HP.test
{
    using static HP.Domain.PersonDomainEvents;
    using static HP.Domain.TodoDomainEvents;
    internal class EventStoreRepositoryTest : TestBase
    {

        [Test]
        public void EventStore_Save_For_TodoCreate()
        {
            var newGuid = Guid.NewGuid();
            IDomainEvent domainEvent = new TodoCreated(newGuid, "HP09428", "Todo Application Event created.", TodoType.Others.Name);
            //_eventStore.SaveEventsAsync(Guid.NewGuid(), 0, domainEvent, 1);
        }
        [Test]
        public void EventStore_Save_For_PersonCreate()
        {
            var addr = new Address("Canada", "Kitchener", "Ontario", "N2L 3M3");
            var domainEvent = new PersonCreated(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }
    }
}
