using HP.Core.Events;
using HP.Domain;
using HP.Infrastructure.Repository;
using HP.Infrastructure;
using HP.test;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HP.Shared.Contacts;

namespace HP.UnitTest.People
{
    public class PersonServiceTest : TestBase
    {
        IPersonService _personService = null;
        IPersonRepository _personRepository = null;
        IEventStore _eventStore = null;
        IEventProducer _eventProducer = null;
        [SetUp]
        public void Setup()
        {
            _eventStore = new EventStore(_mongoDbContext, _eventProducer);
            _personRepository = new PersonRepository(_mongoDbContext, _eventStore);
        }
    }
}
