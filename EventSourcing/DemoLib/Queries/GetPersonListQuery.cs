using DemoLib.Models;
using MediatR;

namespace DemoLib.Queries
{
    public record GetPersonListQuery() : IRequest<List<PersonModel>>;

    //Same with above!
    //public class GetPersonListQueryClass : IRequest<List<PersonModel>>
    //{
    //}
}
