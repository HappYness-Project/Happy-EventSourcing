using HP.Domain;
using MediatR;

namespace HP.Application.Queries.Person
{
    public record GetPersonListQuery() : IRequest<List<Person>>;

}
