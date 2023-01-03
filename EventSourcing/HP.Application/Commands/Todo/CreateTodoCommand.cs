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
        private readonly IMapper _mapper;
        private readonly ITodoRepository _repository;
        private readonly IPersonRepository _personRepository;
        private readonly IEventStore _eventStore;
        public CreateTodoCommandHandler(IMapper mapper, ITodoRepository repository, IPersonRepository personRepository, IEventStore eventStore)
        {
            _mapper = mapper;
            _repository = repository;
            _personRepository = personRepository;
            _eventStore = eventStore;
        }

        public async Task<CommandResult> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPersonByUserIdAsync(request.PersonId.ToString());
            if (person == null)
                throw new ApplicationException($"There is no person for this person. User ID : {request.PersonId.ToString()}");

            var todo = Domain.Todo.Create(person, request.TodoTitle, request.Description, TodoType.FromName(request.TodoType), request.Tag);
            todo.SetStatus(TodoStatus.NotDefined);
            var createdTodo = await _repository.CreateAsync(todo);

            await _eventStore.SaveEventsAsync(createdTodo.Id, createdTodo.UncommittedEvents, createdTodo.Version);
            return new CommandResult(true, "Todo is created.", todo.Id.ToString());
        }
    }
}
