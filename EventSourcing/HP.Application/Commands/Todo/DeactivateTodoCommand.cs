using HP.Core.Commands;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record DeavtivateTodoCommand(Guid TodoId) : BaseCommand;
    public class DeavtivateTodoCommandHandler : IRequestHandler<DeavtivateTodoCommand, CommandResult>
    {
        private readonly ITodoRepository _repository;
        public DeavtivateTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(DeavtivateTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is no Todo ID: {cmd.TodoId}.");

            todo.DeactivateTodo();
            await _repository.UpdateAsync(todo);
            return new CommandResult(true, "Todo is deactiavated", todo.Id.ToString());
        }
    }
}
