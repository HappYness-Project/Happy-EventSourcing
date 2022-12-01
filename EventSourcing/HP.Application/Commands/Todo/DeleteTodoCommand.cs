using HP.Core.Commands;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record DeleteTodoCommand(string Id) : BaseCommand;
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, CommandResult>
    {
        private readonly ITodoRepository _repository;
        public DeleteTodoCommandHandler(ITodoRepository todoRepository)
        {
            _repository = todoRepository;
        }
        public async Task<CommandResult> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.Id);
            if (todo == null)
                throw new ArgumentNullException(nameof(todo));

            await _repository.DeleteByIdAsync(request.Id);
            var @event = new TodoDomainEvents.TodoRemoved(request.Id);
            return new CommandResult(true);
        }
    }
}
