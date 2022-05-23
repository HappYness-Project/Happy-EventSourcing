using HP.Domain.Todos;
using MediatR;

namespace HP.Application.Handlers
{
    public class CompletedTodoCommandHandler : IRequestHandler<CompletedTodoCommand, bool>
    {
        private readonly ITodoRepository _repository;
        public CompletedTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CompletedTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = _repository.GetByIdAsync(request.TodoId)?.Result;
            if (todo == null)
                return false;

            todo.SetStatus(request.TodoId, TodoStatus.Completed);
            await _repository.UpdateAsync(todo);
            return true;
        }
    }
}
