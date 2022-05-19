using HP.Domain.Todos;
using MediatR;

namespace HP.Application.Queries
{
    public class GetTodosByUserIdHandler : IRequestHandler<GetTodosByUserId, IEnumerable<Todo>>
    {
        private readonly ITodoRepository _todoRepository;
        public GetTodosByUserIdHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }
        public async Task<IEnumerable<Todo>> Handle(GetTodosByUserId request, CancellationToken cancellationToken)
        {
             return await _todoRepository.GetListByUserId(request.UserId);
        }
    }
}
