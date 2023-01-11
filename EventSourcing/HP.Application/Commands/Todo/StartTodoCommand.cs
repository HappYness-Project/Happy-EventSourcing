using HP.Core.Commands;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record StartTodoCommand(Guid TodoId) : BaseCommand;
    public class StartTodoCommandHandler : IRequestHandler<StartTodoCommand, CommandResult>
    {
        private readonly ITodoAggregateRepository _repository;
        public StartTodoCommandHandler(ITodoAggregateRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(StartTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetActiveTodoById(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is not active Todo ID: {cmd.TodoId}.");

            todo.SetStatus(TodoStatus.Start);
            await _repository.UpdateAsync(todo);
            return new CommandResult(true, "Todo status has been updated", todo.Id.ToString());
        }
    }
}
