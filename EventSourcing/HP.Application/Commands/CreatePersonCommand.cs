﻿using AutoMapper;
using HP.Application.DTOs;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands
{
    public record CreatePersonCommand(string FirstName, string LastName, Address Address, string emailAddr, string UserName = null) : CommandBase<PersonDetailsDto>;

    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, PersonDetailsDto>
    {
        private readonly IPersonRepository _repository;
        private readonly IMapper _mapper;   
        public CreatePersonCommandHandler(IMapper mapper, IPersonRepository personRepository)
        {
            this._mapper = mapper;
            this._repository = personRepository;
        }
        public async Task<PersonDetailsDto> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _repository.GetPersonByUserIdAsync(request.UserName.ToUpper());
            if(person != null)
                throw new ApplicationException($"The username : {request.UserName} Already exists.");

            var check = await _repository.CreateAsync(Person.Create(request.FirstName, request.LastName, request.Address, request.emailAddr, request.UserName.ToUpper()));
            return _mapper.Map<PersonDetailsDto>(check);
        }
    }
}
