using HP.Core.Events;
using HP.Core.Models;
using HP.Domain;
using HP.Infrastructure;
using HP.Infrastructure.Repository;
using NUnit.Framework;
using System;
namespace HP.test
{
    using static HP.Domain.PersonDomainEvents;
    using static HP.Domain.TodoDomainEvents;
    internal class EventStoreRepositoryTest : TestBase
    {

        private IEventStore _eventStore = null;
        private IEventProducer _eventProducer = null;
        private IEventStoreRepository _eventStoreRepository = null;
        [SetUp]
        public void SetUp()
        {
             _eventStore = new EventStore(_mongoDbContext, _eventProducer);
            _eventStoreRepository = new EventStoreRepository(_mongoDbContext);
        }

        [Test]
        public void EventStore_Save_For_TodoCreate()
        {
            IDomainEvent domainEvent = new TodoCreated(Guid.NewGuid().ToString(), "HP09428", "Todo Application Event created.", TodoType.Others.Name);
            _eventStore.Save(domainEvent);
        }

        [Test]
        public void EventStore_Save_For_PersonCreate()
        {
            var addr = new Address("Canada", "Kitchener", "Ontario", "N2L 3M3");
            IDomainEvent domainEvent = new PersonCreated(Guid.NewGuid().ToString());
            _eventStore.Save(domainEvent);
        }


        //[Test]
        //public void EventStore_Save_For_PersonCreate()
        //{
        //    var addr = new Address("Canada", "Kitchener", "Ontario", "N2L 3M3");
        //    var person = new Person();
        //    var createUserCommand = new CreatePersonCommand("hyunbin7303", "Kevin", "Park", addr);

        //    IDomainEvent domainEvent = new PersonCreated(Guid.NewGuid().ToString(), "Kevin", "Park", "hyunbin7303@gmail.com", addr);
        //    eventStore.SaveEventsAsync(1, 1, null, "PersonCreated");
        //}

        [Test]
        public void eventStore_Save()
        {
            //var events = eventStore.GetEvents<PersonCreated>(1);
        }

    }
}
