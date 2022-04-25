using HP.Domain.Person;
using MediatR;

namespace HP.Application.Queries
{
    public record GetPersonListQuery() : IRequest<List<Person>>;

}
