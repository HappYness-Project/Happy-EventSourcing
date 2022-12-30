using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record DeleteTodoItemCommand(Guid TodoId, Guid TodoItemId) : IRequest<bool>;
    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, bool>
    {
        private readonly ITodoRepository _repository;
        public DeleteTodoItemCommandHandler(ITodoRepository todoRepository)
        {
            _repository = todoRepository;
        }
        public async Task<bool> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.TodoId);
            if (todo == null)
                throw new ApplicationException($"TodoId:{todo.Id} doesn't exist. ");

            var todoItem = todo.SubTodos.Where(x => x.Id == request.TodoItemId).FirstOrDefault();
            if (todoItem == null)
                throw new ApplicationException($"{request.TodoItemId} does not exist in the TodoId: {todo.Id}");

            todo.DeleteTodoItem(request.TodoItemId);
            await _repository.UpdateAsync(todo);
            var @event = new TodoDomainEvents.TodoItemRemoved(request.TodoItemId);
            return true;
        }
    }
}
