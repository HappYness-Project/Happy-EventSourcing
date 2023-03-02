using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record CompleteTodoCommand(Guid TodoId) : BaseCommand;
    public class CompleteTodoCommandHandler : BaseCommandHandler, IRequestHandler<CompleteTodoCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Todo> _todoRepository;
        public CompleteTodoCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Todo> repository) : base(eventProducer)
        {
            _todoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<CommandResult> Handle(CompleteTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByAggregateId<Domain.Todo>(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is no active TodoId: {cmd.TodoId}");

            todo.SetStatus(TodoStatus.Complete);
            todo.DeactivateTodo();
            await _todoRepository.PersistAsync(todo);
            return new CommandResult(true, "Successful", todo.Id.ToString());

        }
    }
}
