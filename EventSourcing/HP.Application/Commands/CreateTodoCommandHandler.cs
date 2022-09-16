using AutoMapper;
using HP.Application.DTOs;
using HP.Domain;
using MediatR;
namespace HP.Application.Handlers
{
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

            var todo = Todo.Create(person, request.TodoTitle, request.Description,request.TodoType, request.Tag);
            var checkTodo = await _repository.CreateAsync(todo);
         //   var @event = new TodoDomainEvents.TodoCreated(todo.Id, person.UserId, request.TodoTitle, request.Description,request.Type);
            return _mapper.Map<TodoDetailsDto>(checkTodo);
        }
    }
}

//https://chaitanyasuvarna.wordpress.com/2021/05/30/event-sourcing-pattern-in-net-core/
//https://devblogs.microsoft.com/cesardelatorre/using-domain-events-within-a-net-core-microservice/
