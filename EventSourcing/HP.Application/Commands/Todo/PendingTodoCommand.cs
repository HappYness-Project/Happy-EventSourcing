using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record PendingTodoCommand(Guid TodoId, string? reason = null) : BaseCommand;
    public class PendingTodoCommandHandler : BaseCommandHandler, IRequestHandler<PendingTodoCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Todo> _todoRepository;
        public PendingTodoCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Todo> repository) : base(eventProducer)
        {
            _todoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<CommandResult> Handle(PendingTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByAggregateId<Domain.Todo>(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"Active Todo ID: {cmd.TodoId} does not exist.");

            todo.SetStatus(TodoStatus.Pending, cmd.reason);
            await _todoRepository.PersistAsync(todo);
            return new CommandResult(true, "Updated successfully", todo.Id.ToString());
        }
    }
}