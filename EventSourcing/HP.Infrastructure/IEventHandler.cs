using static HP.Domain.TodoDomainEvents;

namespace HP.Infrastructure
{
    public interface IEventHandler
    {
        Task On(TodoCreated @event);
        Task On(TodoUpdated @event);
        Task On(TodoActivated @event);
        Task On(TodoDeactivated @event);    
        Task On(TodoRemoved @event);
        Task On(TodoItemCreated @event);
        Task On(TodoItemUpdated @event);
    }
}
