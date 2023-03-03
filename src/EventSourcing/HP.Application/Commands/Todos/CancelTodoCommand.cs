using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todos
{
    public record CancelTodoCommand(Guid TodoId) : BaseCommand;
    public class CancelTodoCommandHandler : BaseCommandHandler, IRequestHandler<CancelTodoCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Todo> _todoRepository;
        public CancelTodoCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Todo> todoRepository) : base(eventProducer)
        {
            _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }
        public async Task<CommandResult> Handle(CancelTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByAggregateId<Domain.Todo>(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"Todo ID: {cmd.TodoId} does not exist.");

            todo.SetStatus(TodoStatus.Stop);
            await _todoRepository.PersistAsync(todo);
            return new CommandResult(true, "Successful", todo.Id.ToString());
        }
    }
}
