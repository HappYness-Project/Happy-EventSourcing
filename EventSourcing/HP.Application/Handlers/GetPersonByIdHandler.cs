
using HP.Application.Queries;
using HP.Domain;
using MediatR;
namespace HP.Application.Handlers
{
    public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, Person>
    {
        private readonly IMediator _mediator;

        public GetPersonByIdHandler(IMediator mediator)
        {
            this._mediator = mediator;
        }
        public async Task<Person> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new GetPersonListQuery());
            var output = results.FirstOrDefault(x => x.Id == request.Id);
            return output;
        }
    }
}
