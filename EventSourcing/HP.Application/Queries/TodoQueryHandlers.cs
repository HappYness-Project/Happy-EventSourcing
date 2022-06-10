using HP.Application.DTOs;
using HP.Domain.Todos;
using MediatR;

namespace HP.Application.Queries
{
    public class TodoQueryHandlers : BaseQueryHandler,
                        IRequestHandler<GetTodosByUserId, IEnumerable<TodoBasicInfoDto>>,
                        IRequestHandler<GetTodoById, Todo>
    {
        private readonly ITodoRepository _todoRepository;
        public TodoQueryHandlers(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoBasicInfoDto>> Handle(GetTodosByUserId request, CancellationToken cancellationToken)
        {
            _mapper.
            return await _todoRepository.GetListByUserId(request.UserId);
        }
        public Task<Todo> Handle(GetTodoById request, CancellationToken cancellationToken)
        {
            return _todoRepository.GetByIdAsync(request.Id);
        }
    }
}
