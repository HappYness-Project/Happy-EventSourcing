﻿using DemoLib.Commands;
using DemoLib.Models;
using HP.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib.Handlers
{

    // Command handler
    public class InsertPersonHandler : IRequestHandler<InsertPersonCommand, Person>
    {
        private readonly IDemoDataAccess _data;
        private readonly IRepository<PersonRepository> _repository;
        public InsertPersonHandler(IDemoDataAccess data)
        {
            this._data = data;
        }
        public Task<Person> Handle(InsertPersonCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_data.InsertPerson(request.FirstName, request.LastName));
        }
    }
}
