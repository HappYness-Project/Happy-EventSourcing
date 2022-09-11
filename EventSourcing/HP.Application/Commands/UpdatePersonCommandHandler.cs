using HP.Domain;
using MediatR;

namespace HP.Application.Commands
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, bool>
    {
        private readonly IPersonRepository _repository;
        public UpdatePersonCommandHandler(IPersonRepository personRepository)
        {
            this._repository = personRepository;
        }
        public Task<bool> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = _repository.GetPersonByUserIdAsync(request.UserId.ToUpper()).Result;
            if (person == null)
                throw new ApplicationException($"UserId : {request.UserId} is not exist.");

            person = Person.UpdateBasicPerson(person, request.FirstName, request.LastName, request.Email);
            var check = _repository.UpdatePersonAsync(person);
            if (check != null)
                return Task.FromResult(true);
            return Task.FromResult(false);
        }
    }
}
