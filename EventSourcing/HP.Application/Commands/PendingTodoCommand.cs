using HP.Domain;
using MediatR;
namespace HP.Application.Commands
{
    public record PendingTodoCommand(string TodoId, string? reason = null) : BaseCommand;
    public class PendingTodoCommandHandler : IRequestHandler<PendingTodoCommand, CommandResult>
    {
        private readonly ITodoRepository _repository;
        public PendingTodoCommandHandler(ITodoRepository repository)
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
            return new CommandResult(true, "Updated successfully", todo.Id);
        }
    }
}