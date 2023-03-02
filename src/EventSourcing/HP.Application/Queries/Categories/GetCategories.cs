using HP.Domain.Categories;
using MediatR;
namespace HP.Application.Queries
{
    public record GetCategories() : IRequest<IEnumerable<Category>>;
}
