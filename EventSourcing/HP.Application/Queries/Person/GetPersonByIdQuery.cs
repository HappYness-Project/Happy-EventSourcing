using MediatR;

namespace HP.Application.Queries.Person
{
    public record PersonDetails
    {
        public PersonDetails(int id, string firstName, string lastName, string address, string email)
        {
        }
    }

    public record GetPersonByIdQuery(string Id) : IRequest<HP.Domain.Person.Person>;
}
