using AutoMapper;
using HP.Core.Common;
using HP.Domain.People.Read;
using MediatR;
namespace HP.Application.Queries
{
    #region QueryModels
    public record GetPersonById(Guid Id) : IRequest<PersonDetails>;
    public record GetPersonByName(string PersonName) : IRequest<PersonDetails>;
    public record GetPersonList() : IRequest<IEnumerable<PersonDetails>>;
    #endregion
    public class PersonQueryHandlers : BaseQueryHandler,
                                        IRequestHandler<GetPersonList, IEnumerable<PersonDetails>>,
                                        IRequestHandler<GetPersonById, PersonDetails>,
                                        IRequestHandler<GetPersonByName, PersonDetails>
    {
        private readonly IBaseRepository<PersonDetails> _personRepository;
Please check out if this is the main reason not work!
        #region Ctors
        public PersonQueryHandlers(IMapper mapper, IBaseRepository<PersonDetails> personRepository) : base(mapper)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        #endregion

        #region handlers
        public async Task<IEnumerable<PersonDetails>> Handle(GetPersonList request, CancellationToken cancellationToken)
        {
            var people = await _personRepository.GetAllAsync();
            if (people == null)
                throw new ApplicationException($"There is no person in the Person Collection.");

            return people;
        }
        public async Task<PersonDetails> Handle(GetPersonById request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.Id);
            if (person == null)
                throw new ApplicationException($"Person not exist. Person ID:{request.Id}");

            return person;
        }
        public async Task<PersonDetails> Handle(GetPersonByName request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.FindOneAsync(x=> x.PersonName == request.PersonName);
            if (person == null)
                throw new ApplicationException($"Person not exist. Person Name:{request.PersonName}");

            return person;
        }
        #endregion
    }
}
