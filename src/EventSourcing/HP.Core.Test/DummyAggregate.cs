using HP.Core.Models;
using static System.Formats.Asn1.AsnWriter;
namespace HP.Core.Test;
public class DummyAggregate : AggregateRoot
{
    public string DummyName { get; private set; }
    public string DummyType { get; private set; }
    public int Score { get; private set; }
    public DummyAggregate() : base(){  }
    public DummyAggregate(string dummyName, string type, int score = 0) : base()
    {
        DummyName = dummyName;
        DummyType = type;
        Score = score;
        AddDomainEvent(new DummyCreatedEvent { DummyName = dummyName, DummyType = type, Score = score });
    }

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
    public void DummyUpdateScore(int newScore)
    {
        Score = newScore;
        AddDomainEvent(new DummyScoreUpdatedEvent { Score = newScore });
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
