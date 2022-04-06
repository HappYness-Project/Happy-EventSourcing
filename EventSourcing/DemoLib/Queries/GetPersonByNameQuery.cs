using HP.Domain.Person;
using MediatR;


namespace DemoLib.Queries
{
    public record GetPersonByNameQuery(int Id) : IRequest<Person>;
}
