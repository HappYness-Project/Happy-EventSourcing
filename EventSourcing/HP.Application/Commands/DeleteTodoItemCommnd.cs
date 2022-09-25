using System.Linq.Expressions;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands
{
    public record DeleteTodoItem(string TodoId, string TodoItemId) : IRequest<bool>;
    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItem, bool>
    {
        private readonly ITodoRepository _repository;
        public DeleteTodoItemCommandHandler(ITodoRepository todoRepository)
        {
            this._repository = todoRepository;
        }
        public async Task<bool> Handle(DeleteTodoItem request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.TodoId);
            if(todo == null)
                throw new ApplicationException($"TodoId:{todo.Id} doesn't exist. ");

            var todoItem = todo.SubTodos.Where(x=> x.Id == request.TodoItemId).FirstOrDefault();
            if(todoItem == null)
                throw new ApplicationException($"{request.TodoItemId} does not exist in the TodoId: {todo.Id}");

            todo.DeleteTodoItem(request.TodoItemId);
            await _repository.UpdateAsync(todo);
         //   await _repository.UpdateAsync(expr);
          //  var @event = new TodoDomainEvents.TodoItemRemoved(request.Id);
            return true;// Publish Remove event. 
        }
    }
}
