using HP.Domain.Person;
using MediatR;

namespace DemoLib.Queries
{
    public record GetPersonListQuery() : IRequest<List<Person>>;

}
