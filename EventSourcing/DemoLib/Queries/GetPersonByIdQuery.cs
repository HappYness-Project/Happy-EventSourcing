using HP.Domain.Person;
using MediatR;

namespace DemoLib.Queries
{
    public record GetPersonByIdQuery(int Id) : IRequest<Person>;
}
