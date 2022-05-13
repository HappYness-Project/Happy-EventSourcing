using HP.Domain;
using MediatR;

namespace HP.Application.Queries
{
    public record GetPersonListQuery() : IRequest<IEnumerable<Person>>;

}
