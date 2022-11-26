using HP.Domain;
using MediatR;
namespace HP.Application.Commands
{
    public record CompleteTodoCommand(string TodoId) : BaseCommand;
    public class CompleteTodoCommandHandler : IRequestHandler<CompleteTodoCommand, CommandResult>
    {
        private readonly ITodoRepository _repository;
        public CompleteTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(CompleteTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetActiveTodoById(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is no active TodoId: {cmd.TodoId}");

            todo.SetStatus(TodoStatus.Complete);
            todo.DeactivateTodo(cmd.TodoId);
            await _repository.UpdateAsync(todo);
            return new CommandResult(true, "Successful", todo.Id);

        }
    }
}
