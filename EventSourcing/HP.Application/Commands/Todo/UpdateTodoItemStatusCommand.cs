using HP.Domain;
using MediatR;

namespace HP.Application.Commands.Todo
{
    public record UpdateTodoItemStatusCommand(Guid TodoId, Guid TodoItemId, string NewStatus) : IRequest<bool>;
    public class UpdateTodoItemStatusCommandHandler : IRequestHandler<UpdateTodoItemStatusCommand, bool>
    {
        private readonly ITodoAggregateRepository _repository;
        public UpdateTodoItemStatusCommandHandler(ITodoAggregateRepository todoRepository)
        {
            _repository = todoRepository;
        }
        public async Task<bool> Handle(UpdateTodoItemStatusCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.TodoId);
            if (todo == null)
                throw new ApplicationException($"TodoId:{todo.Id} doesn't exist.");

            var todoItem = todo.SubTodos.Where(x => x.Id == request.TodoItemId).FirstOrDefault();
            if (todoItem == null)
                throw new ApplicationException($"{request.TodoItemId} does not exist in the TodoId: {todo.Id}");

            todoItem.SetStatus(request.NewStatus);
            await _repository.UpdateAsync(todo);
            return true;
        }
    }
}
