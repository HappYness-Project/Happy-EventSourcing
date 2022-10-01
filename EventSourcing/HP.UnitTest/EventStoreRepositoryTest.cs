﻿using HP.Application.Commands;
using HP.Domain;
using HP.Domain.Common;
using HP.Infrastructure.Repository;
using NUnit.Framework;
using System;
namespace HP.test
{
    using static HP.Domain.PersonEvents;
    using static HP.Domain.TodoDomainEvents;

    internal class EventStoreRepositoryTest : TestBase
    {

        IEventStore eventStore = null;
        [SetUp]
        public void SetUp()
        {
             eventStore = new EventStoreRepository(_configuration, _mongoDbContext);
        }

        [Test]
        public void EventStore_Save_For_TodoCreate()
        {
            IDomainEvent domainEvent = new TodoCreated(Guid.NewGuid().ToString(), "HP09428", "Todo Application Event created.", TodoType.Others);
            eventStore.Save(domainEvent);
        }

        [Test]
        public void EventStore_Save_For_PersonCreate()
        {
            var addr = new Address("Canada", "Kitchener", "Ontario", "N2L 3M3");
            IDomainEvent domainEvent = new PersonCreated(Guid.NewGuid().ToString(), "Kevin", "Park", new Email("hyunbin7303@gmail.com"), addr);
            eventStore.Save(domainEvent);
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
            var events = eventStore.GetEvents<PersonCreated>(1);
        }

    }
}