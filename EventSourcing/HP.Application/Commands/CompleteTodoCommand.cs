using HP.Domain;
using MediatR;
namespace HP.Application.Commands
{
    public record CompleteTodoCommand(string TodoId) : CommandBase<Unit>;
    public class CompleteTodoCommandHandler : IRequestHandler<CompleteTodoCommand, Unit>
    {
        private readonly ITodoRepository _repository;
        public CompleteTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CompleteTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetActiveTodoById(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is no active TodoId: {cmd.TodoId}");

            todo.SetStatus(TodoStatus.Complete);
            await _repository.UpdateAsync(todo);
            return Unit.Value;

        }
    }
}
