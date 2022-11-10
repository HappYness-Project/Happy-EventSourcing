using HP.Domain;
using MediatR;
namespace HP.Application.Commands
{
    public record CompleteTodoCommand(string TodoId) : CommandBase<Unit>;
    public class CompleteTodoCommandHandler : IRequestHandler<CompleteTodoCommand, Unit>
    {
        private readonly ITodoRepository _repository;
        public CompleteTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CompleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = _repository.GetActiveTodoById(request.TodoId)?.Result;
            if (todo == null)
                throw new ApplicationException($"There is no active TodoId: {request.TodoId}");

            todo.SetStatus(TodoStatus.Complete);
            await _repository.UpdateAsync(todo);
            return Unit.Value;

        }
    }
}
