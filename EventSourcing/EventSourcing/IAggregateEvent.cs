namespace EventSourcing
{
    public interface IAggregateEvent<TKey>
    {
        TKey AggregateId { get; }
        int AggregateVersion { get;  }
        DateTime TimeStamp { get; }
    }
}
