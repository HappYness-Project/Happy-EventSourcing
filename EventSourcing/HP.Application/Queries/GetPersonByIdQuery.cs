using HP.Domain;
using MediatR;

namespace HP.Application.Queries
{
    public record PersonDetails
    {
        public PersonDetails(int id, string firstName, string lastName, string address, string email)
        {
        }
    }

    public record GetPersonByIdQuery(int Id) : IRequest<Person>;
}
