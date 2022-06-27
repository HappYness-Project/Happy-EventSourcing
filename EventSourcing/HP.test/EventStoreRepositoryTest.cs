using HP.Application.Events;
using HP.Domain;
using HP.Domain.Common;
using HP.Infrastructure.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HP.Domain.Todos.TodoDomainEvents;

namespace HP.test
{
    internal class EventStoreRepositoryTest : TestBase
    {

        IEventStore eventStore = null;
        [SetUp]
        public void SetUp()
        {
             eventStore = new EventStoreRepository(_configuration, _mongoDbContext);
        }

        [Test]
        public void eventStore_Save()
        {
            IDomainEvent domainEvent = new TodoCreatedEvent("HP09428", Guid.NewGuid().ToString(), "Todo Application Event created.", "Todo Description", "InGeneral");
            eventStore.Save(domainEvent);
        }


        [Test]
        public void eventStore_Save()
        {
            IDomainEvent domainEvent = new TodoCreatedEvent("HP09428", Guid.NewGuid().ToString(), "Todo Application Event created.", "Todo Description", "InGeneral");
            var events = eventStore.GetEvents("");
        }
    }
}
