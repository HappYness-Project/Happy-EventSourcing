using HP.Domain.People.Write;
using MediatR;
namespace HP.Application.Commands.Person
{
    public record UpdatePersonRoleCommand(string UserName, string Role) : IRequest<Unit>;
    public class UpdatePersonRoleCommandHandler : IRequestHandler<UpdatePersonRoleCommand>
    {
        private readonly IPersonAggregateRepository _repository;
        public UpdatePersonRoleCommandHandler(IPersonAggregateRepository personRepository)
        {
            _repository = personRepository;
        }
        public async Task<Unit> Handle(UpdatePersonRoleCommand request, CancellationToken cancellationToken)
        {
            var person = _repository.GetPersonByPersonNameAsync(request.UserName).Result;
            if (person == null)
                throw new ApplicationException($"UserId : {request.UserName} is not exist.");

            person.UpdateRole(request.Role);
            await _repository.UpdateAsync(person);
            return Unit.Value;
        }
    }
}
