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
        public Task<IEnumerable<PersonDetailsDto>> Handle(GetPersonList request, CancellationToken cancellationToken)
        {
            var check = _personRepository.GetAllAsync();
            if (check == null)
                throw new ApplicationException($"There is no person in the Person Collection.");

            return Task.FromResult(_mapper.Map<IEnumerable<PersonDetailsDto>>(check));
        }

        public async Task<PersonDetailsDto> Handle(GetPersonById request, CancellationToken cancellationToken)
        {
            var check = _personRepository.GetByIdAsync(request.Id);
            if (check is null)
                throw new ApplicationException($"Person not exist. Person ID:{request.Id}");

            return _mapper.Map<PersonDetailsDto>(check);
        }
    }
}
