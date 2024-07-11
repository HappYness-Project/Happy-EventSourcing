using HP.Core.Common;
using HP.Core.Test.Dummy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HP.Core.Test;
public class DummyCommandHandlerTests
{
    [Test]
    public async Task ValidCommand_ShouldBe()
    {
        IAggregateRepository<DummyAggregate> aggregateRepo = new DummyAggregateRepository<DummyAggregate>();
        var cmdHandler = new DummyCommandHandler(aggregateRepo);

        var result = await cmdHandler.Handle(new CreateDummyCommand("Testing"), CancellationToken.None);

        //aggregateRepo.Agg
    }
}
