using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todos
{
    public record StartTodoCommand(Guid TodoId) : BaseCommand;
    public class StartTodoCommandHandler : BaseCommandHandler, IRequestHandler<StartTodoCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Todo> _todoRepository;
        public StartTodoCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Todo> repository) : base(eventProducer)
        {
            _todoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<CommandResult> Handle(StartTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByAggregateId<Domain.Todo>(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is not active Todo ID: {cmd.TodoId}.");

            todo.SetStatus(TodoStatus.Start);
            await _todoRepository.PersistAsync(todo);
            return new CommandResult(true, "Todo status has been updated", todo.Id.ToString());
        }
    }
}
