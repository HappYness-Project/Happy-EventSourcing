using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record DeavtivateTodoCommand(Guid TodoId) : BaseCommand;
    public class DeavtivateTodoCommandHandler : BaseCommandHandler, IRequestHandler<DeavtivateTodoCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Todo> _todoRepository;
        public DeavtivateTodoCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Todo> repository) : base(eventProducer)
        {
            _todoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<CommandResult> Handle(DeavtivateTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByAggregateId<Domain.Todo>(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is no Todo ID: {cmd.TodoId}.");

            todo.DeactivateTodo();
            await _todoRepository.PersistAsync(todo);
            return new CommandResult(true, "Todo is deactiavated", todo.Id.ToString());
        }
    }
}
