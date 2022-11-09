using AutoMapper;
using HP.Application.DTOs;
using HP.Domain;
using MediatR;
namespace HP.Application.Queries
{
    public class PersonQueryHandlers : BaseQueryHandler,
                                        IRequestHandler<GetPersonList, IEnumerable<PersonDetailsDto>>,
                                        IRequestHandler<GetPersonById, PersonDetailsDto>,
                                        IRequestHandler<GetPersonByName, PersonDetailsDto>,
                                        IRequestHandler<GetPersonUserId, PersonDetailsDto>
    {
        private readonly IPersonRepository _personRepository;
        public PersonQueryHandlers(IMapper mapper, IPersonRepository personRepository) : base(mapper)
        {
            _personRepository = personRepository;
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
            var check = await _personRepository.GetByIdAsync(request.Id.ToUpperInvariant());
            if (check == null)
                throw new ApplicationException($"Person not exist. Person ID:{request.Id}");

            return _mapper.Map<PersonDetailsDto>(check);
        }

        public async Task<PersonDetailsDto> Handle(GetPersonByName request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<PersonDetailsDto> Handle(GetPersonUserId request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPersonByUserIdAsync(request.UserId.ToUpperInvariant());
            if (person == null)
                throw new ApplicationException($"Person not exist. Person ID:{request.UserId}");

            return _mapper.Map<PersonDetailsDto>(person);
        }
    }
}
