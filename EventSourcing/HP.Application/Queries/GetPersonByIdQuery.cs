using HP.Domain;
using MediatR;

namespace HP.Application.Queries
{
    public record GetPersonByIdQuery(int Id) : IRequest<Person>;
}
