using HP.Core.Commands;
using HP.Core.Events;
using HP.Domain.Todos.Write;
using MediatR;
namespace HP.Application.Commands.Todos
{
    public record DeleteTodoItemCommand(Guid TodoId, Guid TodoItemId) : BaseCommand;
    public class DeleteTodoItemCommandHandler : BaseCommandHandler, IRequestHandler<DeleteTodoItemCommand, CommandResult>
    {
        private readonly ITodoAggregateRepository _repository;
        public DeleteTodoItemCommandHandler(IEventProducer eventProducer, ITodoAggregateRepository todoRepository) : base(eventProducer)
        {
            _repository = todoRepository;
        }
        public async Task<CommandResult> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.TodoId);
            if (todo == null)
                throw new ApplicationException($"TodoId:{todo.Id} doesn't exist. ");

            var todoItem = todo.SubTodos.Where(x => x.Id == request.TodoItemId).FirstOrDefault();
            if (todoItem == null)
                throw new ApplicationException($"{request.TodoItemId} does not exist in the TodoId: {todo.Id}");

            todo.DeleteTodoItem(request.TodoItemId);
            await _repository.UpdateAsync(todo);
            return new CommandResult(true, $"TodoItem is removed from Todo ID: {todo.Id}", request.TodoItemId.ToString());
        }
    }
}
