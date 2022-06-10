using HP.Application.Events;
using HP.Domain.Todos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.EventHandlers
{
    public class TodoEventHandlers : INotificationHandler<TodoCreatedEvent>
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
    }
}
