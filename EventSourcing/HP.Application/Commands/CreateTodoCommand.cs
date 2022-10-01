using AutoMapper;
using HP.Application.Commands;
using HP.Application.DTOs;
using HP.Domain;
using MediatR;

namespace HP.Application.Handlers
{
    public record CreateTodoCommand(string UserId, string TodoTitle, string TodoType, string Description = null,  string[] Tag = null) : CommandBase<TodoDetailsDto>;
    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, TodoDetailsDto>
    {
        private readonly IMapper _mapper;
        private readonly ITodoRepository _repository;
        private readonly IPersonRepository _personRepository;
        public CreateTodoCommandHandler(IMapper mapper, ITodoRepository repository, IPersonRepository personRepository)
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

            var type = TodoType.FromName(request.TodoType);
            var todo = Todo.Create(person, request.TodoTitle, request.Description, type, request.Tag);
            var checkTodo = await _repository.CreateAsync(todo);
            var @event = new TodoDomainEvents.TodoCreated(todo.Id, person.UserId, request.TodoTitle, type);
            return _mapper.Map<TodoDetailsDto>(checkTodo);
        }
    }
}
