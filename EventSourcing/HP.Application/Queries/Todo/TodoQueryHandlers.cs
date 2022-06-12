using HP.Application.DTOs;
using HP.Domain.Todos;
using MediatR;
using AutoMapper;

namespace HP.Application.Queries.Todo
{
    public class TodoQueryHandlers : BaseQueryHandler,
                                     IRequestHandler<GetTodosByUserId, IEnumerable<TodoBasicInfoDto>>,
                                     IRequestHandler<GetTodoById, Todo>

    {
        private readonly ITodoRepository _todoRepository;
        public TodoQueryHandlers(IMapper mapper, ITodoRepository todoRepository) : base(mapper)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoBasicInfoDto>> Handle(GetTodosByUserId request, CancellationToken cancellationToken)
        {
            var todos = await _todoRepository.GetListByUserId(request.UserId);
            if (todos == null)
                throw new ApplicationException($"Todos not exist for this user ID:{request.UserId}");
            return _mapper.Map<List<TodoBasicInfoDto>>(todos);
        }
        public Task<Todo> Handle(GetTodoById request, CancellationToken cancellationToken)
        {
            return _todoRepository.GetByIdAsync(request.Id);
        }
    }
}
