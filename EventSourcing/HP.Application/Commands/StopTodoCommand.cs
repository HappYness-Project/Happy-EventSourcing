using HP.Application.Commands;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands
{
    public record StopTodoCommand(string TodoId, string reason) : CommandBase<Unit>;
    public class StopTodoCommandHandler : IRequestHandler<StopTodoCommand,Unit>
    {
        private readonly ITodoRepository _repository;
        public StopTodoCommandHandler(ITodoRepository   repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(StopTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo =  await _repository.GetActiveTodoById(cmd.TodoId);
            if(todo == null)
                throw new ApplicationException($"Todo ID: {cmd.TodoId} does not exist.");
            
            todo.SetStatus(TodoStatus.Stopped, cmd.reason);
            await _repository.UpdateAsync(todo);
            return Unit.Value;
        }
    }
}
