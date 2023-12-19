using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todos
{
    public record AcceptTodoCommand(Guid TodoId) : BaseCommand;
    public class AcceptTodoCommandHandler : BaseCommandHandler, IRequestHandler<AcceptTodoCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Todo> _todoRepository;
        public AcceptTodoCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Todo> repository) : base(eventProducer)
        {
            _todoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<CommandResult> Handle(AcceptTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.RehydrateAsync<Domain.Todo>(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is not active Todo ID: {cmd.TodoId}.");

            todo.SetStatus(TodoStatus.Accept);
            await _todoRepository.PersistAsync(todo);
            return new CommandResult(true, "Success", todo.Id.ToString());
        }
    }
}
