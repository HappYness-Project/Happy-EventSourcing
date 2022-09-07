using AutoMapper;
using HP.Application.DTOs;
using HP.Domain;
using MediatR;

namespace HP.Application.Commands
{
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
            var person = await _repository.GetPersonByUserIdAsync(request.UserName);
            if(person != null)
            {
                var person = Person.Create(request.FirstName, request.LastName, request.Address, request.emailAddr, request.UserName);
                var check = await _repository.CreateAsync(person);
                return _mapper.Map<PersonDetailsDto>(check);
            }

        }
    }
}

//please read this !!!
//TODO https://ademcatamak.medium.com/layers-in-ddd-projects-bd492aa2b8aa
// This one too! https://matthiasnoback.nl/2021/02/does-it-belong-in-the-application-or-domain-layer/