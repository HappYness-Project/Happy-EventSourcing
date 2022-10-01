using HP.Domain;
using MediatR;
namespace HP.Application.Commands
{
    public record UpdatePersonGroupCommand(string UserName, int GroupId) : IRequest<Unit>;
    public class UpdatePersonGroupCommandHandler : IRequestHandler<UpdatePersonGroupCommand>
    {
        private readonly IPersonRepository _repository;
        public UpdatePersonGroupCommandHandler(IPersonRepository personRepository)
        {
            this._repository = personRepository;
        }
        public async Task<Unit> Handle(UpdatePersonGroupCommand request, CancellationToken cancellationToken)
        {
            var person = _repository.GetPersonByUserIdAsync(request.UserName.ToUpper()).Result;
            if (person == null)
                throw new ApplicationException($"UserId : {request.UserName} is not exist.");

            person.UpdateGroupId(request.GroupId);
            await _repository.UpdateAsync(person);
            return Unit.Value;
        }
    }
}
