using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries
{
    public record GetPersonByName(string PersonName) : IRequest<PersonDetailsDto>;
}
