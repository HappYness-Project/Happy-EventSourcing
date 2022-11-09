using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries
{
    public record GetPersonUserId(string UserId) : IRequest<PersonDetailsDto>;
}
