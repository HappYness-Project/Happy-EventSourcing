using HP.Domain.Todos.Write;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record UpdateStatusTodoItemCommand(Guid TodoId, Guid TodoItemId, string Status) : IRequest<bool>;
    public class UpdateStatusTodoItemCommandHandler : IRequestHandler<UpdateStatusTodoItemCommand, bool>
    {
        private readonly ITodoAggregateRepository _repository;
        public UpdateStatusTodoItemCommandHandler(ITodoAggregateRepository todoRepository)
        {
            _repository = todoRepository;
        }
        public async Task<bool> Handle(UpdateStatusTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.TodoId);
            if (todo == null)
                throw new ApplicationException($"TodoId:{todo.Id} doesn't exist. ");

            var todoItem = todo.SubTodos.Where(x => x.Id == request.TodoItemId).FirstOrDefault();
            if (todoItem == null)
                throw new ApplicationException($"{request.TodoItemId} does not exist in the TodoId: {todo.Id}");

            todoItem.SetStatus(request.Status);
            await _repository.UpdateAsync(todo);
            return true;
        }
    }
}
