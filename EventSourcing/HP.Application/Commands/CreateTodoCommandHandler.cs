using HP.Domain;
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
            if (person is null)
                throw new ApplicationException($"There is no person for this person. User ID : {request.UserId}");

            var todo = Todo.Create(person, request.TodoTitle, request.Description,request.Type, request.Tag);
            var checkTodo = await _repository.CreateAsync(todo);
            var @event = new TodoDomainEvents.TodoCreated(todo.Id, person.UserId, request.TodoTitle, request.Description,request.Type);
            // Publish event ???
            //Stored as inmemory..... as well as database.
            // TODO Send event.
            return null;
        }
    }
}
//https://github.com/IvanMilano/MongoDbTransactionsDemo
//https://chaitanyasuvarna.wordpress.com/2021/05/30/event-sourcing-pattern-in-net-core/
//https://devblogs.microsoft.com/cesardelatorre/using-domain-events-within-a-net-core-microservice/
