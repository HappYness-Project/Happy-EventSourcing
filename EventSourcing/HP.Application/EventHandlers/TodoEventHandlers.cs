using HP.Domain;
using HP.Infrastructure.EventHandlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.EventHandlers
{
    using static HP.Domain.TodoDomainEvents;
    public class TodoEventHandlers : ITodoEventHandler
    {
        private readonly ITodoAggregateRepository _todoRepository;
        public TodoEventHandlers(ITodoAggregateRepository todoRepository)
        {
            _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }
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
