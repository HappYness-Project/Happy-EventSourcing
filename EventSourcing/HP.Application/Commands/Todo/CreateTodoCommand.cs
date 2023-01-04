using AutoMapper;
using HP.Core.Commands;
using HP.Core.Events;
using HP.Domain;
using MediatR;

namespace HP.Application.Commands.Todo
{
    public record CreateTodoCommand(Guid PersonId, string TodoTitle, string TodoType, string? Description = null, DateTime? TargetStartDate = null, DateTime? TargetEndDate = null, string[] Tag = null) : BaseCommand;
    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, CommandResult>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IEventStore _eventStore;
        public CreateTodoCommandHandler(ITodoRepository repository, IPersonRepository personRepository, IEventStore eventStore)
        {
            _todoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _eventStore = eventStore;
        }

        public async Task<CommandResult> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.PersonId);
            if (person == null)
                throw new ApplicationException($"There is no person for this person. User ID : {request.PersonId.ToString()}");

            var todo = Domain.Todo.Create(person, request.TodoTitle, request.Description, TodoType.FromName(request.TodoType), request.Tag);
            todo.SetStatus(TodoStatus.NotDefined);
            var createdTodo = await _todoRepository.CreateAsync(todo);

            await _eventStore.SaveEventsAsync(createdTodo.Id, createdTodo.UncommittedEvents, createdTodo.Version);
            return new CommandResult(true, "Todo is created.", todo.Id.ToString());
        } 
    }
}
