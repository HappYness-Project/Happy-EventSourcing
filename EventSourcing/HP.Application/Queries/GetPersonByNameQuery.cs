using HP.Domain;
using MediatR;


namespace HP.Application.Queries
{
    public record GetPersonByNameQuery(string firstName, string lastName) : IRequest<Person>;
}
