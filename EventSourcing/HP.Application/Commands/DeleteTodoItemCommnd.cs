using System.Linq.Expressions;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands
{
    public record DeleteTodoItem(string Id) : IRequest<bool>;
    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItem, bool>
    {
        private readonly ITodoRepository _repository;
        public DeleteTodoItemCommandHandler(ITodoRepository todoRepository)
        {
            this._repository = todoRepository;
        }
        public async Task<bool> Handle(DeleteTodoItem request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.Id);
            if(todo == null)
                throw new ArgumentNullException(nameof(todo));

            Expression<Func<Todo, bool>> expr = x => x.Id == request.Id;
            await _repository.DeleteOneAsync(expr);
          //  var @event = new TodoDomainEvents.TodoItemRemoved(request.Id);
            return true;// Publish Remove event. 
        }
    }
}
