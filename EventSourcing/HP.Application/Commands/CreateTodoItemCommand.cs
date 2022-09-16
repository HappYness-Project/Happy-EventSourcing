using AutoMapper;
using HP.Application.Commands;
using HP.Application.DTOs;
using HP.Domain;
using MediatR;

namespace HP.Application.Handlers
{
    public record CreateTodoItemCommand(string TodoId, string TodoTitle, string TodoType, string Description = null,  string[] Tag = null) : CommandBase<TodoBasicInfoDto>;
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

        public async Task<TodoDetailsDto> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPersonByUserIdAsync(request.UserId.ToUpper());
            if (person == null)
                throw new ApplicationException($"There is no person for this person. User ID : {request.UserId}");

            var todo = Todo.Create(person, request.TodoTitle, request.Description,request.TodoType, request.Tag);
            var checkTodo = await _repository.CreateAsync(todo);
         //   var @event = new TodoDomainEvents.TodoCreated(todo.Id, person.UserId, request.TodoTitle, request.Description,request.Type);
            return _mapper.Map<TodoDetailsDto>(checkTodo);
        }

        public async Task<TodoBasicInfoDto> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.TodoId);
            if(todo == null)
                throw new ApplicationException($"There is no Todo Id {request.TodoId}");
            
            todo.AddTodoItem(request.TodoTitle, request.TodoType, request.Description);
            throw new NotImplementedException();
        }
    }
    
}
