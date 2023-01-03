using static HP.Domain.TodoDomainEvents;
namespace HP.Infrastructure.EventHandlers
{
    public class EventHandler : ITodoEventHandler
    {
        public Task On(TodoCreated @event)
        {
            throw new NotImplementedException();
        }
        public Task On(TodoUpdated @event)
        {
            throw new NotImplementedException();
        }
        public Task On(TodoActivated @event)
        {
            throw new NotImplementedException();
        }
        public Task On(TodoDeactivated @event)
        {
            throw new NotImplementedException();
        }
        public Task On(TodoRemoved @event)
        {
            throw new NotImplementedException();
        }
        public Task On(TodoItemCreated @event)
        {
            throw new NotImplementedException();
        }
        public Task On(TodoItemUpdated @event)
        {
            throw new NotImplementedException();
        }
    }
}
