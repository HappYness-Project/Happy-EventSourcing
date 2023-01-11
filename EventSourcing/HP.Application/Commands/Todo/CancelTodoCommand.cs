using HP.Core.Commands;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record CancelTodoCommand(Guid todoId) : BaseCommand;
    public class CancelTodoCommandHandler : IRequestHandler<CancelTodoCommand, CommandResult>
    {
        private readonly ITodoAggregateRepository _todoRepository;
        public CancelTodoCommandHandler(ITodoAggregateRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }
        public async Task<CommandResult> Handle(CancelTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.todoId);
            if (todo == null)
                throw new ApplicationException($"Todo ID: {request.todoId} does not exist.");

            todo.SetStatus(TodoStatus.Stop);
            await _todoRepository.UpdateAsync(todo);
            return new CommandResult(true, "Successful", todo.Id.ToString());
        }
    }
}
