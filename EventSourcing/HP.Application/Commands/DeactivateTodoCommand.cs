using HP.Domain;
using MediatR;
namespace HP.Application.Commands
{
    public record DeavtivateTodoCommand(string TodoId) : CommandBase<Unit>;
    public class DeavtivateTodoCommandHandler : IRequestHandler<DeavtivateTodoCommand,Unit>
    {
        private readonly ITodoRepository _repository;
        public DeavtivateTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeavtivateTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo =  await _repository.GetByIdAsync(cmd.TodoId);
            if(todo == null)
                throw new ApplicationException($"There is no Todo ID: {cmd.TodoId}.");
            
            todo.DeactivateTodo(todo.Id);
            await _repository.UpdateAsync(todo);
            return Unit.Value;
        }
    }
}
