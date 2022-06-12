using AutoMapper;
using HP.Domain;
using HP.Domain.Person;
using HP.Infrastructure;
using HP.Infrastructure.Repository;
using MediatR;


namespace HP.Application.Queries.Person
{
    public class PersonQueryHandlers : BaseQueryHandler,
                                        IRequestHandler<GetPersonListQuery, IEnumerable<Person>>,
                                        IRequestHandler<GetPersonByIdQuery, Person>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMediator _mediator;
        public PersonQueryHandlers(IMapper mapper, IMediator mediator, IPersonRepository personRepository) : base(mapper)
        {
            _mediator = mediator;
            _personRepository = personRepository;
        }
        public Task<IEnumerable<Person>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {
            return _personRepository.GetAllAsync();
        }

        public async Task<Person> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new GetPersonListQuery());
            return results.FirstOrDefault(x => x.UserId == request.Id);
        }
    }
}
