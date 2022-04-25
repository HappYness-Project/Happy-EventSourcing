using HP.Domain.Person;
using MediatR;


namespace HP.Application.Queries
{
    public record GetPersonByNameQuery(int Id) : IRequest<Person>;
}
