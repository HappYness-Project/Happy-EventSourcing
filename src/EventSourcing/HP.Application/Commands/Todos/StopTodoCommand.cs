using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todos
{
    public record StopTodoCommand(Guid TodoId, string? reason = null) : BaseCommand;
    public class StopTodoCommandHandler : BaseCommandHandler, IRequestHandler<StopTodoCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Todo> _todoRepository;
        public StopTodoCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Todo> repository) : base(eventProducer)
        {
            _todoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<CommandResult> Handle(StopTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByAggregateId<Domain.Todo>(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"Todo ID: {cmd.TodoId} does not exist.");

            todo.SetStatus(TodoStatus.Stop, cmd.reason);
            await _todoRepository.PersistAsync(todo);
            return new CommandResult(true, "Todo status is changed.", todo.Id.ToString());
        }
    }
}
