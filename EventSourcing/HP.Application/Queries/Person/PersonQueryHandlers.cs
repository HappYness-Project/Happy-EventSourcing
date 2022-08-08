using AutoMapper;
using HP.Application.DTOs;
using HP.Application.Queries.People;
using HP.Application.Queries.Person;
using HP.Domain;
using HP.Domain.Person;
using MediatR;


namespace HP.Application.Queries
{
    public class PersonQueryHandlers : BaseQueryHandler,
                                        IRequestHandler<GetPersonListQuery, IEnumerable<PersonDetailsDto>>,
                                        IRequestHandler<GetPersonByIdQuery, PersonDetailsDto>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMediator _mediator;
        public PersonQueryHandlers(IMapper mapper, IMediator mediator, IPersonRepository personRepository) : base(mapper)
        {
            _mediator = mediator;
            _personRepository = personRepository;
        }
        public Task<IEnumerable<PersonDetailsDto>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {
            var check = _personRepository.GetAllAsync();
            if (check is null)
                return null;

            return Task.FromResult(_mapper.Map<IEnumerable<PersonDetailsDto>>(check));
        }

        public async Task<PersonDetailsDto> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new GetPersonListQuery());
            return results.FirstOrDefault(x => x.UserId == request.Id);
        }
    }
}
