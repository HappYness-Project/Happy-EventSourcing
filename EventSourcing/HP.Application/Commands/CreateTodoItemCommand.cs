using AutoMapper;
using HP.Application.Commands;
using HP.Application.DTOs;
using HP.Domain;
using MediatR;

namespace HP.Application.Handlers
{
    public record CreateTodoItemCommand(string TodoId, string TodoTitle, string TodoType, string? Description,  string[] Tag = null) : CommandBase<TodoBasicInfoDto>;
    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoBasicInfoDto>
    {
        private readonly IMapper _mapper;
        private readonly ITodoRepository _repository;
        private readonly IPersonRepository _personRepository;
        public CreateTodoItemCommandHandler(IMapper mapper, ITodoRepository repository, IPersonRepository personRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _personRepository = personRepository;
        }
        public async Task<TodoBasicInfoDto> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.TodoId);
            if(todo == null)
                throw new ApplicationException($"There is no Todo Id {request.TodoId}");
            
            var subTodo = todo.AddTodoItem(request.TodoTitle, request.TodoType, request.Description);
            await _repository.UpdateAsync(todo);
            return _mapper.Map<TodoBasicInfoDto>(subTodo);
        }
    }
    
}
