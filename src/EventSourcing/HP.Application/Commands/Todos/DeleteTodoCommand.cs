using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using MediatR;
namespace HP.Application.Commands.Todos
{
    public record DeleteTodoCommand(Guid TodoId) : BaseCommand;
    public class DeleteTodoCommandHandler : BaseCommandHandler, IRequestHandler<DeleteTodoCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Todo> _todoRepository;
        public DeleteTodoCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Todo> repository) : base(eventProducer)
        {
            _todoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<CommandResult> Handle(DeleteTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByAggregateId<Domain.Todo>(cmd.TodoId);
            if (todo == null)
                throw new ArgumentNullException(nameof(todo));

            todo.Remove();
            await _todoRepository.PersistAsync(todo);
            return new CommandResult(true);
        }
    }
}
