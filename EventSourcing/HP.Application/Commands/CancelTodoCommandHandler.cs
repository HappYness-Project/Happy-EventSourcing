using HP.Application.Commands;
using HP.Domain.Todos;
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
            Expression<Func<Todo, bool>> hi = null;
            await _repository.DeleteOneAsync(hi);
            return true;
        }
    }
}
