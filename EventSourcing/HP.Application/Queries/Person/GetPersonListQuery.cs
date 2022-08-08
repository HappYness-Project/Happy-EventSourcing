using HP.Application.DTOs;
using MediatR;

namespace HP.Application.Queries.Person
{
    public record GetPersonListQuery() : IRequest<List<PersonDetailsDto>>;

}
