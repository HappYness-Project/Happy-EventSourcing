using HP.Domain;
using HP.Domain.Todos;
using MediatR;
using System.Linq.Expressions;

namespace HP.Application.Commands
{
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, bool>
    {
        private readonly ITodoRepository _repository;
        public DeleteTodoCommandHandler(ITodoRepository todoRepository)
        {
            this._repository = todoRepository;
        }
        public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.Id);
            if(todo == null)
                throw new ArgumentNullException(nameof(todo));

            Expression<Func<Todo, bool>> expr = x => x.Id == request.Id;
            await _repository.DeleteOneAsync(expr);
            var @event = new TodoDomainEvents.TodoRemoved(request.Id);
            // Publish Remove event. 
            return true;
        }
    }
}
