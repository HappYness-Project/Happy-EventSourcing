using HP.Core.Events;
using HP.Core.Handlers;
using HP.Core.Models;
using HP.Domain;

namespace HP.Infrastructure
{
    public class TodoEventSourcingHandler: IEventSourcingHandler<Todo>
    {
        private readonly IEventStore _eventStore;
        public TodoEventSourcingHandler(IEventStore eventStore)
        {
            this._eventStore = eventStore;
        }

        public async Task<Todo> GetByIdAsync(Guid aggregateId)
        {
            var aggregate = new Todo();
            var events = await _eventStore.GetEventsAsync(aggregateId);
            if(events == null || !events.Any()) return aggregate;

            aggregate.Version = events.Select(x => x.Version).Max();
            return aggregate;
        }

        public async Task SaveAsync(IAggregateRoot aggregate)
        {
            throw new NotImplementedException();
        }
    }
}