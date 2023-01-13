using FluentAssertions;
using HP.Core.Events;
using HP.Core.Models;
using HP.Domain;
using HP.Infrastructure.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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
            IDomainEvent domainEvent = new TodoCreated(newGuid, "HP09428", "Todo Application Event created.","Desc", TodoType.Others.Name);
            List<IDomainEvent> events = new List<IDomainEvent>();
            events.Add(domainEvent);
        }
        [Test]
        public void EventStore_Save_For_PersonCreate()
        {
            var domainEvent = new PersonCreated(Guid.NewGuid(), Guid.NewGuid().ToString());
        }
    }
}
