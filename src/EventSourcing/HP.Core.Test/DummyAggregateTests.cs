using FluentAssertions;
using HP.Core.Models;
namespace HP.Core.Test;
public class DummyScoreUpdatedEvent : DomainEvent
{
    public int Score { get; set; }
}
public class DummyCreatedEvent : DomainEvent
{
    public string DummyName { get; set; }
    public string DummyType { get; set; }
    public int Score { get; set; }
}
public class DummyAggregate : AggregateRoot
{
    public string DummyName { get; private set; }
    public string DummyType { get; private set; }
    public int Score { get; private set; }

    public DummyAggregate() : base() {}

    public override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case DummyCreatedEvent created:
                Apply(created);
                break;

            case DummyScoreUpdatedEvent updated:
                Apply(updated);
                break;
        }
    }
    private void Apply(DummyCreatedEvent @event)
    {
        DummyName = @event.DummyName;
        Score = @event.Score;
        DummyType = @event.DummyType;
    }

    private void Apply(DummyScoreUpdatedEvent @event)
    {
        Score = @event.Score;
    }
}

public class DummyAggregateWhenTests
{
    [Test]  
    public void GivenTwoEvents_WhenGetCurrentState_ThenReturnCurrentValue()
    {
        var aggCreated = new DummyCreatedEvent { DummyName = "Dummy", DummyType = "Normal", Score = 80 };

        var aggUpdated = new DummyScoreUpdatedEvent { Score = 100 };

        var events = new object[] {aggCreated, aggUpdated };

        var dummyAggregate = new DummyAggregate();

        foreach(IDomainEvent @event in events)
        {
            dummyAggregate.When(@event);
        }

        dummyAggregate.Id.Should().NotBeEmpty();
        dummyAggregate.DummyName.Should().Be(aggCreated.DummyName);
        dummyAggregate.DummyType.Should().Be(aggCreated.DummyType);
        dummyAggregate.Score.Should().Be(aggUpdated.Score);
    }
}