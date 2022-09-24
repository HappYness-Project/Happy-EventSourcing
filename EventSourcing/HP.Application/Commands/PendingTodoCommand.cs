using HP.Application.Commands;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands
{
    public record PendingTodoCommand(string TodoId) : CommandBase<Unit>;
    public class PendingTodoCommandHandler : IRequestHandler<PendingTodoCommand,Unit>
    {
        private readonly ITodoRepository _repository;
        public PendingTodoCommandHandler(ITodoRepository   repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(PendingTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo =  await _repository.GetByIdAsync(cmd.TodoId);
            if(todo == null)
                throw new ApplicationException($"Todo ID: {cmd.TodoId} does not exist.");
            
            todo.ActivateTodo(todo.Id);
            todo.SetStatus(todo.Id, TodoStatus.Pending);
            await _repository.UpdateAsync(todo);
            return Unit.Value;
        }
    }
}