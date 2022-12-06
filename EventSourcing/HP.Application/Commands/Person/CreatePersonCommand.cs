using AutoMapper;
using HP.Core.Commands;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Person
{
    public record CreatePersonCommand(string PersonId, string PersonType, int? GroupId = null) : BaseCommand;
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, CommandResult>
    {
        private readonly IPersonRepository _repository;
        private readonly IMapper _mapper;   
        public CreatePersonCommandHandler(IMapper mapper, IPersonRepository personRepository)
        {
            this._mapper = mapper;
            this._repository = personRepository;
        }
        public async Task<CommandResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _repository.GetPersonByUserIdAsync(request.PersonId.ToUpper());
            if(person != null)
                throw new ApplicationException($"The PersonId : {request.PersonId} Already exists.");

            var newPerson = await _repository.CreateAsync(Domain.Person.Create(request.PersonId.ToUpper()));
            return new CommandResult(true, "Successfully person has been created.", newPerson.Id);
        }
    }
}
