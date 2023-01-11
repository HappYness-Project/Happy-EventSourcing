using HP.Core.Commands;
using HP.Domain;
using MediatR;

namespace HP.Application.Commands.Todo
{
    public record DeactivateTodoItemCommand(Guid TodoId, Guid TodoItemId) : BaseCommand;
    public class DeactivateTodoItemCommandHandler : IRequestHandler<DeactivateTodoItemCommand, CommandResult>
    {
        private readonly ITodoAggregateRepository _repository;
        public DeactivateTodoItemCommandHandler(ITodoAggregateRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(DeactivateTodoItemCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is no Todo ID: {cmd.TodoId}.");

            TodoItem todoItem = todo.SubTodos.Where(x => x.Id == cmd.TodoItemId).FirstOrDefault();
            if (todoItem == null)
                throw new ApplicationException($"there is no todo Item ID :{cmd.TodoItemId}");

            todoItem.IsActive = false;
            await _repository.UpdateAsync(todo);
            return new CommandResult(true,$"Parent TodoID:{todo.Id}, todoItem:{todoItem.Id} Deactivated.", todoItem.Id.ToString());
        }
    }
}
