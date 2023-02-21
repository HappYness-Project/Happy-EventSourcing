using HP.Core.Commands;
using HP.Domain;
using HP.Domain.Todos.Write;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record StopTodoCommand(Guid TodoId, string? reason = null) : BaseCommand;
    public class StopTodoCommandHandler : BaseCommandHandler, IRequestHandler<StopTodoCommand, CommandResult>
    {
        private readonly ITodoAggregateRepository _repository;
        public StopTodoCommandHandler(ITodoAggregateRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(StopTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetActiveTodoById(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"Todo ID: {cmd.TodoId} does not exist.");

            todo.SetStatus(TodoStatus.Stop, cmd.reason);
            await _repository.UpdateAsync(todo);
            return new CommandResult(true, "Todo status is changed.", todo.Id.ToString());
        }
    }
}
