using HP.Application.IntegrationEvents;
using HP.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.EventHandlers
{
    using static HP.Domain.TodoDomainEvents;
    public class TodoEventHandlers :
                                     IEventHandler<TodoCreated>,
                                     IEventHandler<TodoUpdated>,
                                     IEventHandler<TodoCompleted>,
                                     IEventHandler<TodoActivated>,
                                     IEventHandler<TodoDeactivated>,
                                     IEventHandler<TodoRemoved>


    {
        private readonly ITodoRepository _todoRepository;
        public TodoEventHandlers(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }

        public Task Handle(TodoCreated @event, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task Handle(TodoUpdated notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(TodoCompleted notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(TodoRemoved notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(TodoActivated notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(TodoDeactivated notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


    }
}
