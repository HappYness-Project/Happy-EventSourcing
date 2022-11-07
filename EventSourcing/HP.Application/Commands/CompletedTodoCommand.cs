using HP.Domain;
using MediatR;
namespace HP.Application.Handlers
{
    public record CompletedTodoCommand(string TodoId) : IRequest<bool>;
    public class CompletedTodoCommandHandler : IRequestHandler<CompletedTodoCommand, bool>
    {
        private readonly ITodoRepository _repository;
        public CompletedTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CompletedTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = _repository.GetActiveTodoById(request.TodoId)?.Result;
            if (todo == null)
                throw new ApplicationException($"There is no active TodoId: {request.TodoId}");

            todo.SetStatus(TodoStatus.Complete);
            await _repository.UpdateAsync(todo);
            return true;
        }
    }
}
