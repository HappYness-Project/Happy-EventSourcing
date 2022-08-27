using HP.Application.Commands;
using HP.Domain;
using MediatR;
using System.Linq.Expressions;

namespace HP.Application.Handlers
{
    public class CancelTodoCommandHandler : IRequestHandler<CancelTodoCommand, bool>
    {
        private readonly ITodoRepository _repository;
        public CancelTodoCommandHandler(ITodoRepository todoRepository)
        {
            this._repository = todoRepository;
        }
        public async Task<bool> Handle(CancelTodoCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<Todo, bool>> expr = x => x.Id == request.todoId;
            await _repository.DeleteOneAsync(expr);
            return true;
        }
    }
}
