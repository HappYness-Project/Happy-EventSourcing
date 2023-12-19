using HP.Infrastructure;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.IntegrationTest
{
    public class EventStoreTest : TestBase
    {
        [Test]
        public async Task EventStore_GetAllAggregateId_Success()
        {
            //var aggregateIds = await _eventStore.();
            //Assert.IsNotNull(aggregateIds);
        }

    }
}
