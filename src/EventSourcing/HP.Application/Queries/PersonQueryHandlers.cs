using AutoMapper;
using HP.Application.DTOs;
using HP.Core.Common;
using HP.Domain.People.Read;
using MediatR;
namespace HP.Application.Queries
{
    #region QueryModels
    public record GetPersonById(Guid Id) : IRequest<PersonDetailsDto>;
    public record GetPersonByName(string PersonName) : IRequest<PersonDetailsDto>;
    public record GetPersonList() : IRequest<IEnumerable<PersonDetailsDto>>;
    #endregion
    public class PersonQueryHandlers : BaseQueryHandler,
                                        IRequestHandler<GetPersonList, IEnumerable<PersonDetailsDto>>,
                                        IRequestHandler<GetPersonById, PersonDetailsDto>,
                                        IRequestHandler<GetPersonByName, PersonDetailsDto>
    {
        private readonly IBaseRepository<PersonDetails> _personRepository;
        #region Ctors
        public PersonQueryHandlers(IMapper mapper, IBaseRepository<PersonDetails> personRepository) : base(mapper)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        #endregion

        #region handlers
        public async Task<IEnumerable<PersonDetailsDto>> Handle(GetPersonList request, CancellationToken cancellationToken)
        {
            var people = await _personRepository.GetAllAsync();
            if (people == null)
                throw new ApplicationException($"There is no person in the Person Collection.");

            return _mapper.Map<List<PersonDetailsDto>>(people);
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
            var person = await _personRepository.FindOneAsync(x=> x.PersonName == request.PersonName);
            if (person == null)
                throw new ApplicationException($"Person not exist. Person Name:{request.PersonName}");

            return _mapper.Map<PersonDetailsDto>(person);
        }
        #endregion
    }
}
