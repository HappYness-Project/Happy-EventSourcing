using HP.Application.Commands;
using HP.Domain;
using HP.Infrastructure;
using HP.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.Commands
{

    // Command handler
    public class InsertPersonCommandHandler : IRequestHandler<InsertPersonCommand, Person>
    {
        private readonly IDemoDataAccess _data;
        //private readonly IPersonRepository _repository;
        public InsertPersonCommandHandler(IDemoDataAccess data)
        {
            this._data = data;
        }



        public Task<Person> Handle(InsertPersonCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_data.InsertPerson(request.FirstName, request.LastName));
        }
    }
}


//please read this !!!
//TODO https://ademcatamak.medium.com/layers-in-ddd-projects-bd492aa2b8aa
// This one too! https://matthiasnoback.nl/2021/02/does-it-belong-in-the-application-or-domain-layer/