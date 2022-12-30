using HP.Core.Commands;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record DeleteTodoCommand(Guid TodoId) : BaseCommand;
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, CommandResult>
    {
        private readonly ITodoRepository _repository;
        public DeleteTodoCommandHandler(ITodoRepository todoRepository)
        {
            _repository = todoRepository;
        }
        public async Task<CommandResult> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.TodoId);
            if (todo == null)
                throw new ArgumentNullException(nameof(todo));

            await _repository.DeleteByIdAsync(request.TodoId);
            var @event = new TodoDomainEvents.TodoRemoved(request.TodoId);
            return new CommandResult(true);
        }
    }
}
