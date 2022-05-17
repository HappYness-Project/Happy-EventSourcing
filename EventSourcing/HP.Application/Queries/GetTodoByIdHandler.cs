using HP.Domain.Todos;
using MediatR;

namespace HP.Application.Queries
{
    public class GetTodoByIdHandler : IRequestHandler<GetTodoByIdQuery, Todo>
    {
        private readonly ITodoRepository _todoRepository;
        public GetTodoByIdHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }
        public Task<Todo> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
        {
            return _todoRepository.GetByIdAsync(request.Id);
        }
    }
}
