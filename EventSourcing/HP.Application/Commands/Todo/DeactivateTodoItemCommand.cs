using HP.Domain;
using MediatR;

namespace HP.Application.Commands
{
    public record DeactivateTodoItemCommand(string TodoId, string TodoItemId) : CommandBase<Unit>;
    public class DeactivateTodoItemCommandHandler : IRequestHandler<DeactivateTodoItemCommand, Unit>
    {
        private readonly ITodoRepository _repository;
        public DeactivateTodoItemCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeactivateTodoItemCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is no Todo ID: {cmd.TodoId}.");

            TodoItem todoItem = todo.SubTodos.Where(x => x.Id == cmd.TodoItemId).FirstOrDefault();
            if (todoItem == null)
                throw new ApplicationException($"there is no todo Item ID :{cmd.TodoItemId}");

            todoItem.IsActive = false;
            await _repository.UpdateAsync(todo);
            return Unit.Value;
        }
    }
}
