using HP.Domain.Todos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.Handlers
{
    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Todo>
    {
        private readonly ITodoRepository _repository;
        public CreateTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public Task<Todo> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            Todo todo = new Todo
            {
                Title = request.TodoTitle,
                Description = request.Description,
                Tag = request.Tag,
            };

            var @event = new TodoCreatedEvent(todo.Id, request.UserId);
            // Send event.
            return _repository.CreateAsync(todo);
        }
    }
}
