using HP.Application.DTOs;
using MediatR;
using AutoMapper;
using HP.Domain;

namespace HP.Application.Queries.Todos
{
    public class TodoQueryHandlers : BaseQueryHandler,
                                     IRequestHandler<GetTodos, IEnumerable<TodoBasicInfoDto>>,
                                     IRequestHandler<GetTodosByUserId, IEnumerable<TodoDetailsDto>>,
                                     IRequestHandler<GetTodoById, TodoDetailsDto>,
                                     IRequestHandler<GetTodosByProjectId, IEnumerable<TodoDetailsDto>>,
                                     IRequestHandler<GetTodoItemsByTodoId, IEnumerable<TodoItem>>,
                                     IRequestHandler<GetTodoItemByTodoItemId, TodoItem>

    {
        private readonly ITodoRepository _todoRepository;
        public TodoQueryHandlers(IMapper mapper, ITodoRepository todoRepository) : base(mapper)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoDetailsDto>> Handle(GetTodosByUserId request, CancellationToken cancellationToken)
        {
            var todos = await _todoRepository.GetListByUserId(request.UserId);
            if (todos == null)
                throw new ApplicationException($"Todos not exist for this user ID:{request.UserId}");

            return _mapper.Map<IEnumerable<TodoDetailsDto>>(todos);
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
            if (todoWithProjId == null)
                throw new ApplicationException($"Todo project does not exist. Project ID:{request.ProjectId}");

            return _mapper.Map<List<TodoDetailsDto>>(todoWithProjId);
        }

        public async Task<IEnumerable<TodoBasicInfoDto>> Handle(GetTodos request, CancellationToken cancellationToken)
        {
            var todos = await _todoRepository.GetAllAsync();
            if (todos == null)
                throw new ApplicationException($"Todos Null.");

            return _mapper.Map<List<TodoBasicInfoDto>>(todos);
        }

        public async Task<IEnumerable<TodoItem>> Handle(GetTodoItemsByTodoId request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.todoId);
            if (todo == null)
                throw new ApplicationException($"Cannot find the Todo ID:{request.todoId}");

            return todo.SubTodos;
        }

        public async Task<TodoItem> Handle(GetTodoItemByTodoItemId request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.todoId);
            if (todo == null)
                throw new ApplicationException($"Cannot find the Todo ID:{request.todoId}");

            var todoItem = todo.SubTodos.Where(x => x.Id == request.todoItemId).FirstOrDefault();
            if (todoItem == null)
                throw new ApplicationException($"Cannot find the todo Item: {request.todoItemId}, from todo ID: {request.todoId}");

            return todoItem;
        }
    }
}
