using HP.Application.DTOs;
using MediatR;

namespace HP.Application.Queries
{
    public record GetPersonById(string Id) : IRequest<PersonDetailsDto>;
}
