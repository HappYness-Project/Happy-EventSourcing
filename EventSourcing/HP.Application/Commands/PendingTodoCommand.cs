using HP.Application.Commands;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands
{
    public record PendingTodoCommand(string TodoId, string? reason = null) : CommandBase<Unit>;
    public class PendingTodoCommandHandler : IRequestHandler<PendingTodoCommand,Unit>
    {
        private readonly ITodoRepository _repository;
        public PendingTodoCommandHandler(ITodoRepository   repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(PendingTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo =  await _repository.GetActiveTodoById(cmd.TodoId);
            if(todo == null)
                throw new ApplicationException($"Active Todo ID: {cmd.TodoId} does not exist.");
            
            todo.SetStatus(todo.Id, TodoStatus.Pending, cmd.reason);
            await _repository.UpdateAsync(todo);
            return Unit.Value;
        }
    }
}