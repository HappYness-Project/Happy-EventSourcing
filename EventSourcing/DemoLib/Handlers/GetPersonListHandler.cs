using DemoLib.Models;
using DemoLib.Queries;
using HP.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib.Handlers
{
    public class GetPersonListHandler : IRequestHandler<GetPersonListQuery, List<Person>>
    {
        private readonly IDemoDataAccess _data;
        private readonly IRepository<Person> _personRepository;
        public GetPersonListHandler(IDemoDataAccess data, IRepository<Person> personRepository)
        {
            _data = data;
            _personRepository = personRepository;
        } 
        public Task<List<Person>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_data.GetPeople());
         }
    }
}
