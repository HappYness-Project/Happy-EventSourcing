using HP.Core.Commands;
using HP.Domain;
using HP.Domain.Todos.Write;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record PendingTodoCommand(Guid TodoId, string? reason = null) : BaseCommand;
    public class PendingTodoCommandHandler : BaseCommandHandler, IRequestHandler<PendingTodoCommand, CommandResult>
    {
        private readonly ITodoAggregateRepository _repository;
        public PendingTodoCommandHandler(ITodoAggregateRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(PendingTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetActiveTodoById(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"Active Todo ID: {cmd.TodoId} does not exist.");

            todo.SetStatus(TodoStatus.Pending, cmd.reason);
            await _repository.UpdateAsync(todo);
            return new CommandResult(true, "Updated successfully", todo.Id.ToString());
        }
    }
}