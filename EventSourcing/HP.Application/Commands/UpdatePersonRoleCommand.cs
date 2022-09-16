using HP.Domain;
using MediatR;
namespace HP.Application.Commands
{
    public record UpdatePersonRoleCommand(string UserName, string Role) : IRequest<Unit>;
    public class UpdatePersonRoleCommandHandler : IRequestHandler<UpdatePersonRoleCommand>
    {
        private readonly IPersonRepository _repository;
        public UpdatePersonRoleCommandHandler(IPersonRepository personRepository)
        {
            this._repository = personRepository;
        }
        public async Task<Unit> Handle(UpdatePersonRoleCommand request, CancellationToken cancellationToken)
        {
            var person = _repository.GetPersonByUserIdAsync(request.UserName.ToUpper()).Result;
            if (person == null)
                throw new ApplicationException($"UserId : {request.UserName} is not exist.");

            person.UpdateRole(request.Role);
            await _repository.UpdateAsync(person);
            return Unit.Value;
        }
    }
}
