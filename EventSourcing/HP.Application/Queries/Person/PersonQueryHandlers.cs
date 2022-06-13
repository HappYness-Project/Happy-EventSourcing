using AutoMapper;
using HP.Application.Queries.Person;
using HP.Domain;
using HP.Domain.Person;
using HP.Infrastructure;
using HP.Infrastructure.Repository;
using MediatR;


namespace HP.Application.Queries
{
    public class PersonQueryHandlers : BaseQueryHandler,
                                        IRequestHandler<GetPersonListQuery, IEnumerable<HP.Domain.Person.Person>>,
                                        IRequestHandler<GetPersonByIdQuery, HP.Domain.Person.Person>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMediator _mediator;
        public PersonQueryHandlers(IMapper mapper, IMediator mediator, IPersonRepository personRepository) : base(mapper)
        {
            _mediator = mediator;
            _personRepository = personRepository;
        }
        public Task<IEnumerable<HP.Domain.Person.Person>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {
            return _personRepository.GetAllAsync();
        }

        public async Task<HP.Domain.Person.Person> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new GetPersonListQuery());
            return results.FirstOrDefault(x => x.UserId == request.Id);
        }
    }
}
