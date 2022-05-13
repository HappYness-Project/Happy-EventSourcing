using HP.Domain;
using HP.Infrastructure;
using HP.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.Queries
{
    public class GetPersonListHandler : IRequestHandler<GetPersonListQuery, IEnumerable<Person>>
    {
        private readonly IPersonRepository _personRepository;
        public GetPersonListHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Task<IEnumerable<Person>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {
            return _personRepository.GetAllAsync();
        }
    }
}
