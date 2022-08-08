using HP.Application.DTOs;
using MediatR;

namespace HP.Application.Queries.People
{

    public record GetPersonByIdQuery(string Id) : IRequest<PersonDetailsDto>;
}
