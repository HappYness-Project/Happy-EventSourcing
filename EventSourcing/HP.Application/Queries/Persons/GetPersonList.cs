using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries
{
    public record GetPersonList() : IRequest<IEnumerable<PersonDetailsDto>>;

}
