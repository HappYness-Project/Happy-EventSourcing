using HP.Domain;
using MediatR;
namespace HP.Application.Commands
{
    public record StartTodoCommand(string TodoId) : CommandBase<Unit>;
    public class StartTodoCommandHandler : IRequestHandler<StartTodoCommand,Unit>
    {
        private readonly ITodoRepository _repository;
        public StartTodoCommandHandler(ITodoRepository   repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(StartTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo =  await _repository.GetActiveTodoById(cmd.TodoId);
            if(todo == null)
                throw new ApplicationException($"There is not active Todo ID: {cmd.TodoId}.");
            
            todo.SetStatus(todo.Id, TodoStatus.Started);
            await _repository.UpdateAsync(todo);
            return Unit.Value;
        }
    }
}
