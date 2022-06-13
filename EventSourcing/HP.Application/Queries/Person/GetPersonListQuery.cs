using MediatR;

namespace HP.Application.Queries.Person
{
    public record GetPersonListQuery() : IRequest<List<HP.Domain.Person.Person>>;

}
