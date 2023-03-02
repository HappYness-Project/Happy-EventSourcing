using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record ActivateTodoCommand(Guid TodoId) : BaseCommand;
    public class ActivateTodoCommandHandler : BaseCommandHandler, IRequestHandler<ActivateTodoCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Todo> _todoRepository;
        public ActivateTodoCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Todo> repository) : base(eventProducer)
        {
            _todoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<CommandResult> Handle(ActivateTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByAggregateId<Domain.Todo>(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is no Todo ID: {cmd.TodoId}.");

            todo.ActivateTodo();
            await _todoRepository.PersistAsync(todo);
            return new CommandResult(true, "Successful", todo.Id.ToString());
        }
    }
}
