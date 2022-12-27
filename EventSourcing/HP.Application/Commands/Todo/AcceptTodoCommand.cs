using HP.Core.Commands;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record AcceptTodoCommand(Guid TodoId) : BaseCommand;
    public class AcceptTodoCommandHandler : IRequestHandler<AcceptTodoCommand, CommandResult>
    {
        private readonly ITodoRepository _repository;
        public AcceptTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(AcceptTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetActiveTodoById(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is not active Todo ID: {cmd.TodoId}.");

            todo.SetStatus(TodoStatus.Accept);
            await _repository.UpdateAsync(todo);
            return new CommandResult(true, "Success", todo.Id.ToString());
        }
    }
}
