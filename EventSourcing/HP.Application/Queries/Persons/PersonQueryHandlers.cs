using AutoMapper;
using HP.Application.DTOs;
using HP.Domain.People.Write;
using MediatR;
namespace HP.Application.Queries
{
    public class PersonQueryHandlers : BaseQueryHandler,
                                        IRequestHandler<GetPersonList, IEnumerable<PersonDetailsDto>>,
                                        IRequestHandler<GetPersonById, PersonDetailsDto>,
                                        IRequestHandler<GetPersonByName, PersonDetailsDto>,
                                        IRequestHandler<GetPersonUserId, PersonDetailsDto>
    {
        private readonly IPersonAggregateRepository _personRepository;
        //private readonly IPersonRepository _personRepository;
        public PersonQueryHandlers(IMapper mapper, IPersonAggregateRepository personRepository) : base(mapper)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        public async Task<IEnumerable<PersonDetailsDto>> Handle(GetPersonList request, CancellationToken cancellationToken)
        {
            var people = await _personRepository.GetAllAsync();
            if (people == null)
                throw new ApplicationException($"There is no person in the Person Collection.");

            return _mapper.Map<IEnumerable<PersonDetailsDto>>(people);
        }

        public async Task<PersonDetailsDto> Handle(GetPersonById request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.Id);
            if (person == null)
                throw new ApplicationException($"Person not exist. Person ID:{request.Id}");

            return _mapper.Map<PersonDetailsDto>(person);
        }

        public async Task<PersonDetailsDto> Handle(GetPersonByName request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPersonByPersonNameAsync(request.PersonName);
            if (person == null)
                throw new ApplicationException($"Person not exist. Person Name:{request.PersonName}");

            return _mapper.Map<PersonDetailsDto>(person);
        }

        public async Task<PersonDetailsDto> Handle(GetPersonUserId request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPersonByPersonNameAsync(request.UserId.ToUpperInvariant());
            if (person == null)
                throw new ApplicationException($"Person not exist. Person ID:{request.UserId}");

            return _mapper.Map<PersonDetailsDto>(person);
        }
    }
}
