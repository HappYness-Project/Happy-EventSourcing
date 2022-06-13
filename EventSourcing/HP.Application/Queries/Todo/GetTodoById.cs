using MediatR;
namespace HP.Application.Queries
{
    public record GetTodoById(string Id) : IRequest<Domain.Todos.Todo>;

}
