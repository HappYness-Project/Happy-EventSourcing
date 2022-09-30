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
            var todo =  await _repository.GetByIdAsync(cmd.TodoId);
            if(todo == null)
                throw new ApplicationException($"Todo ID: {cmd.TodoId} does not exist.");
            
            todo.ActivateTodo(todo.Id);
            todo.SetStatus(todo.Id, TodoStatus.Started);
            await _repository.UpdateAsync(todo);
            return Unit.Value;
        }
    }
}
