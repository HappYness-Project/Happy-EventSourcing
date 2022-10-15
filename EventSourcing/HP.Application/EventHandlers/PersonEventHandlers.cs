using HP.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.EventHandlers
{
    using static HP.Domain.PersonEvents;
    public class PersonEventHandlers : INotificationHandler<PersonCreated>

    {
        private readonly IPersonRepository _personRepository;
        public PersonEventHandlers(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public Task Handle(PersonCreated notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
