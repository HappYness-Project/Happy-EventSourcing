using HP.Domain;
using MediatR;
namespace HP.Application.Commands
{
    public record ActivateTodoCommand(string TodoId) : CommandBase<Unit>;
    public class ActivateTodoCommandHandler : IRequestHandler<ActivateTodoCommand,Unit>
    {
        private readonly ITodoRepository _repository;
        public ActivateTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(ActivateTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo =  await _repository.GetByIdAsync(cmd.TodoId);
            if(todo == null)
                throw new ApplicationException($"There is no Todo ID: {cmd.TodoId}.");
            
            todo.ActivateTodo(todo.Id);
            await _repository.UpdateAsync(todo);
            return Unit.Value;
        }
    }
}
