﻿using DemoLib.Queries;
using HP.Domain.Person;
using HP.Infrastructure;
using HP.Infrastructure.Repository;
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
        private readonly IPersonRepository _personRepository;
        public GetPersonListHandler(IDemoDataAccess data, IPersonRepository personRepository)
        {
            _data = data;
            _personRepository = personRepository;
        } 
        public Task<List<Person>> Handle(GetPersonL   
            return Task.FromResult(_data.GetPeople());
        }
    }
}
