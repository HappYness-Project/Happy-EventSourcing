using HP.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.EventHandlers
{
    using static HP.Domain.PersonDomainEvents;
    public class PersonEventHandlers : INotificationHandler<PersonCreated>

    {
        private readonly IPersonAggregateRepository _personRepository;
        public PersonEventHandlers(IPersonAggregateRepository personRepository)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        public Task Handle(PersonCreated notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
