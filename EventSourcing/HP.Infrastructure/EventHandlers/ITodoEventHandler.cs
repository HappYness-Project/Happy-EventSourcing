using HP.Domain;
using MediatR;
using static HP.Domain.TodoDomainEvents;
namespace HP.Infrastructure.EventHandlers
{
    public interface ITodoEventHandler : INotification
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
