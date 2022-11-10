using HP.Domain;
using MediatR;
namespace HP.Application.Commands
{
    public record AcceptTodoCommand(string TodoId) : CommandBase<Unit>;
    public class AcceptTodoCommandHandler : IRequestHandler<AcceptTodoCommand, Unit>
    {
        private readonly ITodoRepository _repository;
        public AcceptTodoCommandHandler(ITodoRepository   repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AcceptTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo =  await _repository.GetActiveTodoById(cmd.TodoId);
            if(todo == null)
                throw new ApplicationException($"There is not active Todo ID: {cmd.TodoId}.");
            
            todo.SetStatus(TodoStatus.Accept);
            await _repository.UpdateAsync(todo);
            return Unit.Value;
        }
    }
}
