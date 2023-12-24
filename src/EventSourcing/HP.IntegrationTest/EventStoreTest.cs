using EventStore.Client;
using FluentAssertions;
using HP.Core.Events;
using HP.Infrastructure;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task GivenInvalidStream_EventsShouldBeZero()
        {
            var result = await _eventStore.GetEventsAsync(Guid.NewGuid(), "NotExistingStream");
            
            result.Count().Should().Be(0);
        }

    }
}
