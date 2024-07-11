using HP.Core.Models;
using HP.Core.Test.Dummy;
namespace HP.Core.Test;

public class DummyAggregateTests
{
    [Test]
    public void GivenTwoEvents_WhenDummyAggregateWhenCalled_ThenAggregateUpdated()
    {
        var aggCreated = new DummyCreatedEvent { DummyName = "Dummy", DummyType = "Normal", Score = 80 };
        var aggUpdated = new DummyScoreUpdatedEvent { Score = 100 };
        var events = new object[] { aggCreated, aggUpdated };
        var dummyAggregate = new DummyAggregate();

        foreach (IDomainEvent @event in events)
            dummyAggregate.When(@event);

        dummyAggregate.Id.Should().NotBeEmpty();
        dummyAggregate.DummyName.Should().Be(aggCreated.DummyName);
        dummyAggregate.DummyType.Should().Be(aggCreated.DummyType);
        dummyAggregate.Score.Should().Be(aggUpdated.Score);
    }

    [Test]
    public void GivenMultipleEvents_WhenCountUncommittedEvents_ThenCountEquals()
    {
        var dummyAggregate = new DummyAggregate("DummyName", "Normal", 50);
        int howManyUpdateEvents = 5;

        for(int i = 1; i <= howManyUpdateEvents; i++)
            dummyAggregate.DummyUpdateScore(90 + i);

        dummyAggregate.UncommittedEvents.Count().Should().Be(6);
    }
}