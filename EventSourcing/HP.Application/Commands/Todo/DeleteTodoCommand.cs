using HP.Core.Commands;
using HP.Core.Events;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record DeleteTodoCommand(Guid TodoId) : BaseCommand;
    public class DeleteTodoCommandHandler : BaseCommandHandler, IRequestHandler<DeleteTodoCommand, CommandResult>
    {
        private readonly ITodoAggregateRepository _todoRepository;
        public DeleteTodoCommandHandler(IEventProducer eventProducer, ITodoAggregateRepository todoRepository) : base(eventProducer)
        {
            _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }
        public async Task<CommandResult> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.TodoId);
            if (todo == null)
                throw new ArgumentNullException(nameof(todo));

            todo.Remove();
            await _todoRepository.DeleteByIdAsync(request.TodoId);

            await ProduceDomainEvents(todo.UncommittedEvents);
            return new CommandResult(true);
        }
    }
}
