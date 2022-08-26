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
            var person = _repository.GetByIdAsync(request.UserId).Result;
            if (person == null)
                return null;

            var address = Person.CreateAddress("Canada", "Sample", "", "");
            Person.Update(request.FirstName, request.LastName, request.Email, address);
            //person.Email = new Email(request.Email);
            var check = _repository.UpdatePersonAsync(person);
            if (check != null)
                return Task.FromResult(true);
            return Task.FromResult(false);
        }
    }
}
