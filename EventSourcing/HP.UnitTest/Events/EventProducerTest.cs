using HP.Core.Events;
using HP.Infrastructure.Kafka;
using HP.test;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HP.Domain.TodoDomainEvents;

namespace HP.UnitTest.Events
{
    public class EventProducerTest : TestBase
    {

        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void GetCollection_For_EventStore()
        {
            string userId = Guid.NewGuid().ToString();
            TodoCreated todoCreated = new TodoCreated(Guid.NewGuid(), userId, "TodoTitle : new TOdo","Desc", "General");

            _eventProducer.ProducerAsync(todoCreated);
        }

    }
}
