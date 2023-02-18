using AutoMapper;
using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record CreateTodoCommand(Guid PersonId, string TodoTitle, string TodoType, string? Description = null, DateTime? TargetStartDate = null, DateTime? TargetEndDate = null, string[] Tag = null) : BaseCommand;
    public class CreateTodoCommandHandler : BaseCommandHandler, IRequestHandler<CreateTodoCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Todo> _todoRepository;
        private readonly IAggregateRepository<Domain.Person> _personRepository;
        public CreateTodoCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Todo> repository, IAggregateRepository<Domain.Person> personRepository) : base(eventProducer)
        {
            _todoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        public async Task<CommandResult> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByAggregateId<Domain.Person>(request.PersonId);
            if (person == null)
                throw new ApplicationException($"There is no person for this person. User ID : {request.PersonId.ToString()}");

            var todo = Domain.Todo.Create(person, request.TodoTitle, request.Description, TodoType.FromName(request.TodoType), request.Tag);
            todo.SetStatus(TodoStatus.NotDefined);

            await _todoRepository.PersistAsync(todo, cancellationToken);
            await ProduceDomainEvents(todo.UncommittedEvents);
            return new CommandResult(true, "Todo is created.", todo.Id.ToString());
        } 
    }
}
