using DemoLib.Models;
using MediatR;


namespace DemoLib.Queries
{
    public record GetPersonByNameQuery(int Id) : IRequest<Person>;
}
