using HP.Domain.Person;
using MediatR;

namespace HP.Application.Commands
{
    public class InsertPersonCommandHandler : IRequestHandler<InsertPersonCommand, Person>
    {
        private readonly IPersonRepository _repository;
        public InsertPersonCommandHandler(IPersonRepository personRepository)
        {
            this._repository = personRepository;
        }
        public Task<Person> Handle(InsertPersonCommand request, CancellationToken cancellationToken)
        {
            // await event service. PersistAsync
            Person person = new Person
            {
                UserId = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                IsActive = true,

            };
            return _repository.CreateAsync(person);
        }
    }
}


//please read this !!!
//TODO https://ademcatamak.medium.com/layers-in-ddd-projects-bd492aa2b8aa
// This one too! https://matthiasnoback.nl/2021/02/does-it-belong-in-the-application-or-domain-layer/