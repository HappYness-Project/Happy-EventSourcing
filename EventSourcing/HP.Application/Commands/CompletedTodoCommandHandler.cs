using HP.Domain.Todos;
using MediatR;

namespace HP.Application.Handlers
{
    public class CompletedTodoCommandHandler : IRequestHandler<CompletedTodoCommand, Todo>
    {
        private readonly ITodoRepository _repository;
        public CompletedTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public Task<Todo> Handle(CompletedTodoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
