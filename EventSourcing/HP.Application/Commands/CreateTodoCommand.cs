using AutoMapper;
using HP.Application.Commands;
using HP.Application.DTOs;
using HP.Domain;
using MediatR;

namespace HP.Application.Commands
{
    public record CreateTodoCommand(string UserId, string TodoTitle, string TodoType, string? Description = null, DateTime? TargetStartDate = null, DateTime? TargetEndDate = null, string[] Tag = null) : BaseCommand;
    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, CommandResult>
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

        public async Task<CommandResult> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPersonByUserIdAsync(request.UserId.ToUpper());
            if (person == null)
                throw new ApplicationException($"There is no person for this person. User ID : {request.UserId}");

            var todo = Todo.Create(person, request.TodoTitle, request.Description, TodoType.FromName(request.TodoType), request.Tag);
            todo.SetStatus(TodoStatus.NotDefined);
            var checkTodo = await _repository.CreateAsync(todo);
            return new CommandResult(true, "Todo is created.", todo.Id);
        }
    }
}
