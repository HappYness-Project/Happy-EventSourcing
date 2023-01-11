using HP.Core.Commands;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record ActivateTodoCommand(Guid TodoId) : BaseCommand;
    public class ActivateTodoCommandHandler : IRequestHandler<ActivateTodoCommand, CommandResult>
    {
        private readonly ITodoAggregateRepository _repository;
        public ActivateTodoCommandHandler(ITodoAggregateRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(ActivateTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is no Todo ID: {cmd.TodoId}.");

            todo.ActivateTodo();
            await _repository.UpdateAsync(todo);
            return new CommandResult(true, "Successful", todo.Id.ToString());
        }
    }
}
