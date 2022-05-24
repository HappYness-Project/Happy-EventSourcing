using HP.Domain;
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
        private readonly IPersonRepository _personRepository;
        public CreateTodoCommandHandler(ITodoRepository repository, IPersonRepository personRepository)
        {
            _repository = repository;
            _personRepository = personRepository;
        }

        public async Task<Todo> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var person = _personRepository.GetByIdAsync(request.UserId).Result;
            if (person == null)
                return null;

            Todo todo = new Todo(request.UserId, request.TodoTitle, request.Type, request.Tag);

            var @event = new TodoDomainEvents.TodoCreatedEvent(todo.Id, person.UserId, request.TodoTitle, request.Type);
            // TODO Send event.
            return await _repository.CreateAsync(todo);
        }
    }
}
