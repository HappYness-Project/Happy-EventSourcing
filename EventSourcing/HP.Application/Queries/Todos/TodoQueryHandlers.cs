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
                                     IRequestHandler<GetActiveTodoItemsByTodoId, IEnumerable<TodoItem>>,
                                     IRequestHandler<GetTodoItemByTodoItemId, TodoItem>,
                                     IRequestHandler<GetCompletedTodoItemsByTodoId, IEnumerable<TodoItemDto>>,
                                     IRequestHandler<GetPendingTodoItemsByTodoId, IEnumerable<TodoItemDto>>,
                                     IRequestHandler<GetStartedTodoItemsByTodoId, IEnumerable<TodoItemDto>>,
                                     IRequestHandler<GetStoppedTodoItemsByTodoId, IEnumerable<TodoItemDto>>
        
    {
        private readonly ITodoRepository _todoRepository;
        public TodoQueryHandlers(IMapper mapper, ITodoRepository todoRepository) : base(mapper)
        {
            _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }

        public async Task<IEnumerable<TodoDetailsDto>> Handle(GetTodosByUserId request, CancellationToken cancellationToken)
        {
            var todos = await _todoRepository.GetListByPersonName(request.UserId);
            if (todos == null)
                throw new ApplicationException($"Todos not exist for this user ID:{request.UserId}");

            return _mapper.Map<IEnumerable<TodoDetailsDto>>(todos);
        }
        public async Task<TodoDetailsDto> Handle(GetTodoById request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.Id);
            if (todo is null)
                throw new ApplicationException($"TodoId:{request.Id} does not exist.");

            todo.SubTodos.Where(x => x.IsActive);
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

        public async Task<IEnumerable<TodoItem>> Handle(GetActiveTodoItemsByTodoId request, CancellationToken cancellationToken)
        {
            Todo todo = await _todoRepository.GetActiveTodoById(request.todoId);
            if (todo == null)
                throw new ApplicationException($"Cannot find the Todo ID:{request.todoId}");

            return todo.SubTodos.Where(x => x.IsDone);
        }

        public async Task<TodoItem> Handle(GetTodoItemByTodoItemId request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetActiveTodoById(request.TodoId);
            if (todo == null)
                throw new ApplicationException($"Cannot find the Todo ID:{request.TodoId}");

            var todoItem = todo.SubTodos.Where(x => x.Id == request.TodoItemId).FirstOrDefault();
            if (todoItem == null)
                throw new ApplicationException($"Cannot find the todo Item: {request.TodoItemId}, from todo ID: {request.TodoId}");

            return todoItem;
        }
        public async Task<IEnumerable<TodoItemDto>> Handle(GetCompletedTodoItemsByTodoId request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetActiveTodoById(request.TodoId);
            if (todo == null)
                throw new ApplicationException($"Cannot find the Todo ID:{request.TodoId}");

            var completedItems = todo.SubTodos.Where(x => x.TodoStatus !=null && (x.TodoStatus.ToString() == TodoStatus.Complete.Name && x.IsDone));
            var todoItems = _mapper.Map<List<TodoItemDto>>(completedItems);
            return todoItems;
        }
        public async Task<IEnumerable<TodoItemDto>> Handle(GetPendingTodoItemsByTodoId request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetActiveTodoById(request.todoId);
            if (todo == null)
                throw new ApplicationException($"Cannot find the Todo ID:{request.todoId}");

            var completedItems = todo.SubTodos.Where(x => x.TodoStatus.Name == TodoStatus.Pending.Name);
            var todoItems = _mapper.Map<List<TodoItemDto>>(completedItems);
            return todoItems;
        }

        public async Task<IEnumerable<TodoItemDto>> Handle(GetStartedTodoItemsByTodoId request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetActiveTodoById(request.TodoId);
            if (todo == null)
                throw new ApplicationException($"Cannot find the Todo ID:{request.TodoId}");

            var startedItems = todo.SubTodos.Where(x => x.TodoStatus.Name == TodoStatus.Start.Name);
            var todoItems = _mapper.Map<List<TodoItemDto>>(startedItems);
            return todoItems;
        }

        public async Task<IEnumerable<TodoItemDto>> Handle(GetStoppedTodoItemsByTodoId request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetActiveTodoById(request.TodoId);
            if (todo == null)
                throw new ApplicationException($"Cannot find the Todo ID:{request.TodoId}");

            var startedItems = todo.SubTodos.Where(x => x.TodoStatus.Name == TodoStatus.Stop.Name);
            var todoItems = _mapper.Map<List<TodoItemDto>>(startedItems);
            return todoItems;
        }
    }
}
