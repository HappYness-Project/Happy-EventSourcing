using MongoDB.Bson;
namespace HP.Core.Models
{
    public abstract class Entity : BaseEntity
    { 
        public DateTime CreatedDate { get; private set; }
        public Entity()
        {
            Id = ObjectId.GenerateNewId().ToString();
            CreatedDate = DateTime.Now;
        }
        public Entity(string id)
        {
            Id = id;
            CreatedDate = DateTime.Now;
        }
        public int Version { get; set; }
        private List<IDomainEvent> _domainEvents = new();
        public IEnumerable<IDomainEvent> GetUncommittedChanges()
        {
            return _domainEvents?.AsReadOnly();
        }
        public void MarkChangesAsCommitted()
        {
            _domainEvents.Clear();
        }
        private void AddDomainEvent(IDomainEvent @domainEvent, bool isNew)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            var method = this.GetType().GetMethod("Apply", new Type[] { @domainEvent.GetType() });
            if(method == null)
                throw new ArgumentNullException(nameof(method), $"This AddDomainEvent method was not found in the aggregate for {domainEvent.GetType().Name}");
            
            method.Invoke(this, new object[] {@domainEvent});
            if(isNew)
                _domainEvents.Add(@domainEvent);
        }
        protected abstract void When(IDomainEvent @event);
        protected void ApplyChange(IDomainEvent @event)
        {
            AddDomainEvent(@event, true);
        }
        public void ReplayEvents(IEnumerable<IDomainEvent> @events)
        {
            foreach(var @event in events)
            {
                AddDomainEvent(@event, false);
            }
        }
    }
}
