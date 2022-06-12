using HP.Domain.Person;
using MediatR;


namespace HP.Application.Queries.Person
{
    public record GetPersonByNameQuery(string firstName, string lastName) : IRequest<Person>;
}
