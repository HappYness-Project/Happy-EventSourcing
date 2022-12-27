using HP.Core.Commands;
using HP.Domain;
using MediatR;

namespace HP.Application.Commands.Todo
{
    public record ActivateTodoItemCommand(Guid TodoId, Guid TodoItemId) : BaseCommand;
    public class ActivateTodoItemCommandHandler : IRequestHandler<ActivateTodoItemCommand, CommandResult>
    {
        private readonly ITodoRepository _repository;
        public ActivateTodoItemCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(ActivateTodoItemCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is no Todo ID: {cmd.TodoId}.");

            TodoItem todoItem = todo.SubTodos.Where(x => x.Id == cmd.TodoItemId).FirstOrDefault();
            if (todoItem == null)
                throw new ApplicationException($"there is no todo Item ID :{cmd.TodoItemId}");

            todoItem.IsActive = true;
            await _repository.UpdateAsync(todo);
            return new CommandResult(true, $"ParentTodo:{todo.Id}, TodoItem is actiavated", todoItem.Id.ToString());
        }
    }
}
