using HP.Domain.Person;
using MediatR;

namespace HP.Application.Commands
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Person>
    {
        private readonly IPersonRepository _repository;
        public UpdatePersonCommandHandler(IPersonRepository personRepository)
        {
            this._repository = personRepository;
        }
        public Task<Person> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = _repository.GetByIdAsync(request.UserId).Result;
            if (person == null)
                return null;

            //var address = person.CreateAddress("", "", "", "");
            person.Update(request.FirstName, request.LastName, request.Email);
            person.Email = new Email(request.Email);
            return _repository.UpdatePersonAsync(person);
        }
    }
}
