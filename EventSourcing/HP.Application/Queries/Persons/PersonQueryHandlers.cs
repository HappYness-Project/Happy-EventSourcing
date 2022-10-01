using AutoMapper;
using HP.Application.DTOs;
using HP.Domain;
using MediatR;


namespace HP.Application.Queries
{
    public class PersonQueryHandlers : BaseQueryHandler,
                                        IRequestHandler<GetPersonList, IEnumerable<PersonDetailsDto>>,
                                        IRequestHandler<GetPersonById, PersonDetailsDto>
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
            var check = await _personRepository.GetByIdAsync(request.Id);
            if (check == null)
                throw new ApplicationException($"Person not exist. Person ID:{request.Id}");

            return _mapper.Map<PersonDetailsDto>(check);
        }
    }
}
