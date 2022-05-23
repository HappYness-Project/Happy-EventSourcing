using HP.Domain;
namespace HP.Domain.Common
{
    public abstract record DomainEventBase : IDomainEvent
    {
        public DomainEventBase()
        {
            OccuredOn = DateTime.Now;
        }
        public DateTime OccuredOn { get; }
        public string EntityId { get; }
        public string Type { get; }
        public string EntityType { get; }
    }
}

