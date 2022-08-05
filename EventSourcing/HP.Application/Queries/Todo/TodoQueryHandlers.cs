using HP.Application.DTOs;
using HP.Domain.Todos;
using MediatR;
using AutoMapper;
namespace HP.Application.Queries.Todo
{
    public class TodoQueryHandlers : BaseQueryHandler,
                                     IRequestHandler<GetTodosByUserId, IEnumerable<TodoBasicInfoDto>>,
                                     IRequestHandler<GetTodoById, TodoDetailsDto>,
                                     IRequestHandler<GetTodosByProjectId, IEnumerable<TodoDetailsDto>>

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
        public async Task<TodoDetailsDto> Handle(GetTodoById request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.Id);
            if (todo is null)
                throw new ApplicationException($"TodoId:{request.Id} does not exist.");

            return _mapper.Map<TodoDetailsDto>(todo);
        }

        public async Task<IEnumerable<TodoDetailsDto>> Handle(GetTodosByProjectId request, CancellationToken cancellationToken)
        {
            var todos = await _todoRepository.GetAllAsync();
            var todoWithProjId = todos.Where(x => x.ProjectId == request.ProjectId);
            if (todoWithProjId is null)
                throw new ApplicationException($"Todo project does not exist. Project ID:{request.ProjectId}");

            return _mapper.Map<List<TodoDetailsDto>>(todoWithProjId);
        }
    }
}
