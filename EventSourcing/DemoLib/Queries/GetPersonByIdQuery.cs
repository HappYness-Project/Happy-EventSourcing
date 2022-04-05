using DemoLib.Models;
using MediatR;

namespace DemoLib.Queries
{
    public record GetPersonByIdQuery(int Id) : IRequest<Person>;
}
