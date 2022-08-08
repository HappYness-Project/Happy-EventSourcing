using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries.People
{
    public record GetPersonByNameQuery(string firstName, string lastName) : IRequest<PersonDetailsDto>;
}
