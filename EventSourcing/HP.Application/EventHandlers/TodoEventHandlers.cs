using HP.Domain.Todos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HP.Domain.Todos.TodoDomainEvents;

namespace HP.Application.EventHandlers
{
    public class TodoEventHandlers : INotificationHandler<TodoCreatedEvent>,
                                     INotificationHandler<TodoUpdatedEvent>,
                                     INotificationHandler<TodoCompletedEvent>,
                                     INotificationHandler<TodoActivatedEvent>,
                                     INotificationHandler<TodoDeactivatedEvent>,
                                     INotificationHandler<TodoRemovedEvent>

    {
        private readonly ITodoRepository _todoRepository;
        public TodoEventHandlers(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public Task Handle(TodoCreatedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(TodoUpdatedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(TodoCompletedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(TodoRemovedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(TodoActivatedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(TodoDeactivatedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
