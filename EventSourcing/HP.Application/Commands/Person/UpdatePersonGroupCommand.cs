using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Person
{
    public record UpdatePersonGroupCommand(string PersonName, int GroupId) : IRequest<Unit>;
    public class UpdatePersonGroupCommandHandler : IRequestHandler<UpdatePersonGroupCommand>
    {
        private readonly IPersonAggregateRepository _repository;
        public UpdatePersonGroupCommandHandler(IPersonAggregateRepository personRepository)
        {
            _repository = personRepository;
        }
        public async Task<Unit> Handle(UpdatePersonGroupCommand request, CancellationToken cancellationToken)
        {
            var person = _repository.GetPersonByPersonNameAsync(request.PersonName).Result;
            if (person == null)
                throw new ApplicationException($"PersonName : {request.PersonName} is not exist.");

            person.UpdateGroupId(request.GroupId);
            await _repository.UpdateAsync(person);
            return Unit.Value;
        }
    }
}
